﻿using DoomLauncher.Interfaces;
using DoomLauncher.SaveGame;
using DoomLauncher.SourcePort;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DoomLauncher
{
    class SaveGameHandler
    {
        public SaveGameHandler(IDataSourceAdapter adapter, LauncherPath savegameDirectory)
        {
            DataSourceAdapter = adapter;
            SaveGameDirectory = savegameDirectory;
        }

        public IEnumerable<IFileData> HandleNewSaveGames(ISourcePortData sourcePort, IGameFile gameFile, string[] files)
        {
            List<IFileData> ret = new List<IFileData>();

            if (gameFile != null && gameFile.GameFileID.HasValue)
            {
                foreach (string file in files)
                {
                    try
                    {
                        FileInfo fi = new FileInfo(file);
                        string fileName = Guid.NewGuid().ToString() + fi.Extension;
                        fi.CopyTo(Path.Combine(SaveGameDirectory.GetFullPath(), fileName));

                        FileData fileData = new FileData
                        {
                            Description = GetSaveGameName(sourcePort, fi),
                            OriginalFileName = fi.Name,
                            FileName = fileName,
                            GameFileID = gameFile.GameFileID.Value,
                            SourcePortID = sourcePort.SourcePortID,
                            FileTypeID = FileType.SaveGame
                        };

                        DataSourceAdapter.InsertFile(fileData);
                        ret.Add(fileData);
                    }
                    catch
                    {
                        //failed, nothing to do
                    }
                }
            }

            return ret;
        }

        private static string GetSaveGameName(ISourcePortData sourcePort, FileInfo fi)
        {
            ISaveGameReader reader = CreateSaveGameReader(sourcePort, fi);

            if (reader != null)
                return reader.GetName();

            return fi.Name;
        }

        private static ISaveGameReader CreateSaveGameReader(ISourcePortData sourcePort, FileInfo fi)
        {
            return SourcePortUtil.CreateSourcePort(sourcePort).CreateSaveGameReader(fi);
        }

        public void HandleUpdateSaveGames(ISourcePortData sourcePort, IGameFile gameFile, IFileData[] files)
        {
            foreach (IFileData file in files)
            {
                FileInfo fi = new FileInfo(Path.Combine(sourcePort.GetReadSavePath().GetFullPath(), file.OriginalFileName));

                if (fi.Exists)
                {
                    if (file.DateCreated == fi.LastWriteTime)
                        continue;

                    try
                    {
                        fi.CopyTo(Path.Combine(SaveGameDirectory.GetFullPath(), file.FileName), true);
                    }
                    catch
                    {
                        //failed, nothing to do
                    }

                    //check to see if the save name changed
                    string saveName = GetSaveGameName(sourcePort, fi);
                    if (saveName != file.Description)
                        file.Description = saveName;

                    file.DateCreated = fi.LastWriteTime;
                    DataSourceAdapter.UpdateFile(file);
                }
            }
        }

        public void HandleDeleteSaveGames(string[] deletedFiles, IFileData[] previousFiles)
        {
            foreach (var file in deletedFiles)
            {
                FileInfo fi = new FileInfo(file);
                IFileData saveFile = previousFiles.FirstOrDefault(x => x.OriginalFileName == fi.Name);
                if (saveFile != null)
                    DataSourceAdapter.DeleteFile(saveFile);
            }
        }

        public void CopySaveGamesToSourcePort(ISourcePortData sourcePort, IFileData[] files)
        {
            foreach (IFileData file in files)
            {
                string savePath = sourcePort.GetReadSavePath().GetFullPath();
                string fileName = Path.Combine(sourcePort.GetReadSavePath().GetFullPath(), file.OriginalFileName);
                FileInfo fiFrom = new FileInfo(Path.Combine(SaveGameDirectory.GetFullPath(), file.FileName));

                try
                {
                    if (fiFrom.Exists)
                    {
                        DirectoryInfo di = new DirectoryInfo(Path.Combine(savePath, file.OriginalFilePath));

                        if (!di.Exists)
                            di.Create();

                        fiFrom.CopyTo(fileName, true);
                    }
                }
                catch
                {
                    //failed, nothing to do
                }
            }
        }

        public LauncherPath SaveGameDirectory { get; set; }
        public IDataSourceAdapter DataSourceAdapter { get; set; }
    }
}
