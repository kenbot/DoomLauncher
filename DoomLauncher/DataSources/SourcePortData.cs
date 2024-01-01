﻿using DoomLauncher.Interfaces;
using System;
using System.IO;

namespace DoomLauncher.DataSources
{
    public class SourcePortData : ISourcePortData
    {
        public int SourcePortID { get; set; }
        public string Name { get; set; }
        public string SupportedExtensions { get; set; }
        public string SettingsFiles { get; set; }
        public string Executable { get; set; }
        public SourcePortLaunchType LaunchType { get; set; }
        public string FileOption { get; set; }
        public string ExtraParameters { get; set; }
        public LauncherPath AltSaveDirectory { get; set; }
        public bool Archived { get; set; }
        public LauncherPath Directory { get; set; }

        public string GetFullExecutablePath()
        {
            return Path.Combine(Directory.GetFullPath(), Executable);
        }

        public LauncherPath GetReadSavePath()
        {
            if (!string.IsNullOrEmpty(AltSaveDirectory.GetFullPath()))
                return AltSaveDirectory;

            return Directory;
        }

        public LauncherPath GetLoadSavePath()
        {
            if (!string.IsNullOrEmpty(AltSaveDirectory.GetFullPath()))
                return AltSaveDirectory;

            return LauncherPath.NoPath;
        }

        public override bool Equals(object obj)
        {
            ISourcePortData sourcePort = obj as ISourcePortData;

            if (sourcePort != null)
            {
                return sourcePort.SourcePortID == SourcePortID;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return SourcePortID;
        }

        public static string[] GetSupportedExtensions(ISourcePortData sourcePort)
        {
            string[] supportedExtensions = new string[] { };

            if (sourcePort != null)
                supportedExtensions = sourcePort.SupportedExtensions.Split(new string[] { ", ", "," }, StringSplitOptions.RemoveEmptyEntries);

            return supportedExtensions;
        }
    }
}
