﻿using DoomLauncher.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoomLauncher
{
    public partial class SpecificFilesForm : Form
    {
        public class SpecificFilePath
        {
            public string ExtractedFile { get; set; }
            public string InternalFilePath { get; set; }
        }

        private bool m_select, m_autoSelect, m_loading, m_closing;
        private string[] m_specificFiles = new string[] { };
        private string[] m_supportedExtensions = new string[] { };
        private List<IGameFile> m_gameFiles;
        private LauncherPath m_directory, m_temp;
        private readonly List<SpecificFilePath> m_filePaths = new List<SpecificFilePath>();
        private CancellationTokenSource m_ct;
        private List<string> m_items = new List<string>();
        private List<int> m_checkedItems = new List<int>();

        public SpecificFilesForm()
        {
            InitializeComponent();

            AutoCheckSupportedExtensions(true);
            ShowPkContentsCheckBox(false);
            lblLoading.Text = string.Empty;
            txtSearch.PreviewKeyDown += TxtSearch_PreviewKeyDown;

            Stylizer.Stylize(this, DesignMode, StylizerOptions.SetupTitleBar);
        }

        private void TxtSearch_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                HandleSearch();
        }

        public void AutoCheckSupportedExtensions(bool set)
        {
            m_autoSelect = set;
        }

        public void ShowPkContentsCheckBox(bool set)
        {
            DpiScale dpiScale = new DpiScale(CreateGraphics());
            chkPkContents.Visible = set;
            tblMain.RowStyles[0].Height = (set ? dpiScale.ScaleIntY(80) : dpiScale.ScaleIntY(24));
        }

        public void Initialize(LauncherPath gameFileDirectory, IEnumerable<IGameFile> gameFiles, string[] supportedExtensions, string[] specificFiles)
        {
            Initialize(gameFileDirectory, gameFiles, supportedExtensions, specificFiles, null);
        }

        public void Initialize(LauncherPath gameFileDirectory, IEnumerable<IGameFile> gameFiles, string[] supportedExtensions, string[] specificFiles, LauncherPath tempDirectory)
        {
            if (specificFiles != null)
                m_specificFiles = specificFiles.ToArray();
            m_supportedExtensions = supportedExtensions.ToArray();
            m_gameFiles = gameFiles.ToList();
            m_directory = gameFileDirectory;
            m_temp = tempDirectory;

            try
            {
                SetGrid();
            }
            catch(Exception ex)
            {
                Util.DisplayUnexpectedException(this, ex);
            }
        }

        private async void SetGrid()
        {
            if (!m_loading)
            {
                chkPkContents.Enabled = btnSearch.Enabled = false;
                m_loading = true;
                lblLoading.Text = "Loading...";
                clbFiles.Items.Clear();
                m_items = new List<string>();
                m_checkedItems = new List<int>();

                m_ct = new CancellationTokenSource();
                await Task.Run(() => SetGridTask());

                clbFiles.Items.AddRange(m_items.ToArray());
                m_checkedItems.ForEach(x => clbFiles.SetItemChecked(x, true));

                if (!m_closing)
                {
                    lblLoading.Text = string.Empty;
                    m_loading = false;
                    chkPkContents.Enabled = btnSearch.Enabled = true;
                }
            }
        }

        private void SetGridTask()
        {
            m_filePaths.Clear();

            foreach (IGameFile gameFile in m_gameFiles)
            {
                string path = Path.Combine(m_directory.GetFullPath(), gameFile.FileName);
                using (IArchiveReader reader = CreateArchiveReader(gameFile, path))
                {
                    if (m_ct.IsCancellationRequested)
                        break;

                    try
                    {
                        if (m_specificFiles == null || m_specificFiles.Length == 0)
                        {
                            HandleDefaultSelection(path, reader);
                            continue;
                        }

                        foreach (IArchiveEntry entry in reader.Entries)
                        {
                            if (string.IsNullOrEmpty(entry.Name) || entry.IsDirectory)
                                continue;

                            if (m_ct.IsCancellationRequested)
                                break;

                            HandleAddItem(path, entry.FullName, entry.Name, m_specificFiles.Contains(entry.FullName));
                        }
                    }
                    catch
                    {
                        //this is laziness, sometimes it throws an exception when the form is closing in the middle of loading. But were closing the form so who cares
                    }
                }
            }
        }

        private static IArchiveReader CreateArchiveReader(IGameFile gameFile, string path)
        {
            // this ignored pk3 files specifically for some reason
            if (!File.Exists(path) && !Directory.Exists(path))
                return ArchiveReader.EmptyArchiveReader;

            bool isPackagedArchive = ArchiveUtil.ShouldReadPackagedArchive(path);
            if (gameFile.IsUnmanaged() && !isPackagedArchive)
                return new FileArchiveReader(path);

            // TODO IsPk necessary?
            if (isPackagedArchive)
                return ArchiveReader.Create(path);

            if (ArchiveReader.IsPk(Path.GetExtension(path)))
                return new ZipArchiveReader(path);

            return ArchiveReader.EmptyArchiveReader;
        }

        private void HandleDefaultSelection(string file, IArchiveReader reader)
        {
            IEnumerable<IArchiveEntry> filteredEntries;
            
            if (reader is FileArchiveReader)
                filteredEntries = reader.Entries;
            else
                filteredEntries = reader.Entries.Where(x => !string.IsNullOrEmpty(x.Name) && !x.Name.EndsWith(Path.PathSeparator.ToString()) &&
                    m_supportedExtensions.Any(y => y.Equals(Path.GetExtension(x.Name), StringComparison.OrdinalIgnoreCase)))
                    .OrderBy(x => x.Name, StringComparer.OrdinalIgnoreCase).ToList();

            if (chkPkContents.Checked)
            {
                IEnumerable<IArchiveEntry> pk3Entries = Util.GetEntriesByExtension(reader, Util.GetPkExtenstions()); //should check for MAPINFO (e.g. joyofmapping)

                foreach (IArchiveEntry entry in pk3Entries)
                {
                    string extractedFile = Util.ExtractTempFile(m_temp.GetFullPath(), entry);
                    using (IArchiveReader zaInner = ArchiveReader.Create(extractedFile))
                        HandleDefaultSelection(extractedFile, zaInner);
                }
            }

            foreach (IArchiveEntry entry in reader.Entries)
            {
                if (string.IsNullOrEmpty(entry.Name) || entry.IsDirectory)
                    continue;

                if (m_ct.IsCancellationRequested)
                    break;

                HandleAddItem(file, entry.FullName, entry.Name, filteredEntries.Contains(entry));
            }
        }

        private void HandleAddItem(string file, string fullname, string name, bool isChecked)
        {
            isChecked = isChecked && m_autoSelect;

            try
            {
                if (chkSupported.Checked)
                {
                    if (m_supportedExtensions.Any(x => x.Equals(Path.GetExtension(name), StringComparison.OrdinalIgnoreCase)))
                    {
                        if (isChecked)
                            m_checkedItems.Add(m_items.Count);
                        m_items.Add(fullname);
                        m_filePaths.Add(new SpecificFilePath { ExtractedFile = file, InternalFilePath = fullname });
                    }
                }
                else
                {
                    if (isChecked)
                        m_checkedItems.Add(m_items.Count);
                    m_items.Add(fullname);
                    m_filePaths.Add(new SpecificFilePath { ExtractedFile = file, InternalFilePath = fullname });
                }
            }
            catch
            {
                //i dunno
            }
        }

        public static string[] GetSupportedFiles(string gameFileDirectory, IGameFile gameFile, string[] supportedExtensions)
        {
            List<string> files = new List<string>();
            string path = Path.Combine(gameFileDirectory, gameFile.FileName);

            // Directories do not have extensions, always add it
            // Unmanaged pk3s should not be extracted
            if (gameFile.IsDirectory() || (gameFile.IsUnmanaged() && !ArchiveUtil.ShouldReadPackagedArchive(gameFile.FileName)))
            {
                files.Add(gameFile.FileName);
                return files.ToArray();
            }

            using (IArchiveReader reader = CreateArchiveReader(gameFile, path))
            {
                foreach (IArchiveEntry entry in reader.Entries)
                {
                    if (string.IsNullOrEmpty(entry.Name))
                        continue;

                    if (!supportedExtensions.Any(x => x.Equals(Path.GetExtension(entry.Name), StringComparison.OrdinalIgnoreCase)))
                        continue;
                    
                    files.Add(entry.FullName);
                }
            }

            return files.ToArray();
        }

        public string[] GetSpecificFiles()
        {
            return clbFiles.CheckedItems.Cast<string>().ToArray();
        }

        public List<SpecificFilePath> GetPathedSpecificFiles()
        {
            List<SpecificFilePath> ret = new List<SpecificFilePath>();
            for (int i = 0; i < clbFiles.Items.Count; i++)
            {
                if (clbFiles.GetItemChecked(i))
                    ret.Add(m_filePaths.First(x => x.InternalFilePath.Equals(clbFiles.Items[i])));
            }

            return ret;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            HandleSearch();
        }

        private void HandleSearch()
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                var items = m_items.Where(x => x.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) != -1);
                clbFiles.Items.Clear();
                clbFiles.Items.AddRange(items.ToArray());
            }
            else
            {
                clbFiles.Items.AddRange(m_items.ToArray());
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (chkPkContents.Visible && GetSpecificFiles().Length == 0) //really only need to validate if selecting a file to open w/utility
            {
                MessageBox.Show(this, "At least one file must be selected.", "No Files Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void SpecificFilesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_ct != null)
            {
                m_closing = true;
                m_ct.Cancel();
            }
        }

        private void chkPkContents_CheckedChanged(object sender, EventArgs e)
        {
            SetGrid();
        }

        private void lnkSelect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for(int i = 0; i < clbFiles.Items.Count; i++)
                clbFiles.SetItemChecked(i, m_select);

            m_select = !m_select;
        }

        private void chkSupported_CheckedChanged(object sender, EventArgs e)
        {
            SetGrid();
        }
    }
}
