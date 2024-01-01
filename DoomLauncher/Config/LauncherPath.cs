﻿using System;
using System.IO;

namespace DoomLauncher
{
    public class LauncherPath
    {
        public static readonly LauncherPath NoPath = new LauncherPath(string.Empty);

        private readonly string m_path, m_fullPath;

        public LauncherPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                m_path = string.Empty;
                m_fullPath = string.Empty;
                return;
            }

            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                path += Path.DirectorySeparatorChar;

            m_path = path;
            m_fullPath = m_path;

            if (!Path.IsPathRooted(m_fullPath))
            {
                m_fullPath = Path.Combine(Directory.GetCurrentDirectory(), m_fullPath);
            }
            else
            {
                string current = Directory.GetCurrentDirectory();
                if (m_path.StartsWith(current))
                    m_path = m_path.Replace(current, string.Empty);
            }
        }

        public static string GetDataDirectory()
        {
            if (IsInstalled())
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DoomLauncher");

            return Directory.GetCurrentDirectory();
        }

        public static bool IsInstalled() => !File.Exists(Path.Combine(Directory.GetCurrentDirectory(), DbDataSourceAdapter.DatabaseFileName)) && !File.Exists(Path.Combine(Directory.GetCurrentDirectory(), DbDataSourceAdapter.InitDatabaseFileName));

        public string GetFullPath()
        {
            return m_fullPath;
        }

        public string GetPossiblyRelativePath()
        {
            return m_path;
        } 

        public static implicit operator string(LauncherPath p)
        {
            Util.ThrowDebugException("implicit operator should not be assumed");
            return p.GetFullPath();
        }

        public override bool Equals(object obj)
        {
            if (obj is LauncherPath path)
                return GetFullPath() == path.GetFullPath();

            return false;
        }

        public override int GetHashCode()
        {
            return GetFullPath().GetHashCode();
        }
    }
}
