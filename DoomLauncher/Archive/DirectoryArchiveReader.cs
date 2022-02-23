﻿using System.Collections.Generic;
using System.IO;

namespace DoomLauncher
{
    public class DirectoryArchiveReader : IArchiveReader
    {
        private readonly string m_directory;
        private readonly List<DirectoryArchiveEntry> m_entries = new List<DirectoryArchiveEntry>();

        public IEnumerable<IArchiveEntry> Entries => m_entries;

        public DirectoryArchiveReader(string directory)
        {
            m_directory = directory;
            foreach (string file in Directory.EnumerateFiles(m_directory))
                m_entries.Add(new DirectoryArchiveEntry(file));
        }

        void RecursivelyIterateDirectory(string directory)
        {
            try
            {
                foreach (string dir in Directory.GetDirectories(directory))
                {
                    foreach (string file in Directory.GetFiles(dir))
                        m_entries.Add(new DirectoryArchiveEntry(file));
                    RecursivelyIterateDirectory(dir);
                }
            }
            catch
            {
            }
        }

        public void Dispose()
        {
            
        }
    }
}
