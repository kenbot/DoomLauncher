﻿using DoomLauncher.Interfaces;
using DoomLauncher.SaveGame;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DoomLauncher.SourcePort
{
    public class GenericSourcePort : ISourcePort
    {
        protected readonly ISourcePortData m_sourcePortData;

        public GenericSourcePort(ISourcePortData sourcePortData)
        {
            m_sourcePortData = sourcePortData;
        }

        public bool CheckFileNameWithoutExtension(string name) =>
            Path.GetFileNameWithoutExtension(m_sourcePortData.Executable).Equals(name, StringComparison.InvariantCultureIgnoreCase);

        public bool CheckFileNameContains(string name) =>
            CultureInfo.CurrentCulture.CompareInfo.IndexOf(m_sourcePortData.Executable, name) >= 0;

        public virtual string IwadParameter(SpData data) => $" -iwad \"{data.Value}\"";

        public virtual string FileParameter(SpData data)
        {
            if (!string.IsNullOrEmpty(m_sourcePortData.FileOption))
                return string.Concat(" ", m_sourcePortData.FileOption, " ");
            return string.Empty;
        }

        public virtual string WarpParameter(SpData data) => BuildWarpParameter(data.Value);

        public virtual string SkillParameter(SpData data) =>
            $" -skill {data.Value}";

        public virtual string RecordParameter(SpData data) =>
            $" -record \"{data.Value}\"";

        public virtual string PlayDemoParameter(SpData data) =>
            $" -playdemo \"{data.Value}\"";

        public virtual string LoadSaveParameter(SpData data)
        {
            if (string.IsNullOrEmpty(data.Value))
                return string.Empty;

            string file = Path.GetFileNameWithoutExtension(data.Value);
            if (!char.IsDigit(file[file.Length - 1]))
                return string.Empty;

            return $"-loadgame {file[file.Length - 1]}";
        }

        public static string BuildWarpParameter(string map)
        {
            if (Regex.IsMatch(map, @"^E\dM\d$") || Regex.IsMatch(map, @"^MAP\d\d$"))
                return BuildWarpLegacy(map);

            return GetMapParameter(map);
        }

        public static string GetMapParameter(string map) => $" +map {map}";

        public virtual bool Supported() => true;

        public virtual bool StatisticsSupported() => false;

        public virtual bool LoadSaveGameSupported() => true;

        public virtual string[] GetScreenshotDirectories() => Array.Empty<string>();

        public virtual string[] GetSaveGameDirectories() => Array.Empty<string>();

        public virtual IStatisticsReader CreateStatisticsReader(IGameFile gameFile, IEnumerable<IStatsData> existingStats) => null;

        public virtual ISaveGameReader CreateSaveGameReader(FileInfo file)
        {
            if (file.Extension.Equals(".zds", StringComparison.InvariantCultureIgnoreCase))
                return new ZDoomSaveGameReader(file.FullName);
            else if (file.Extension.Equals(".dsg", StringComparison.InvariantCultureIgnoreCase))
                return new DsgSaveGameReader(file.FullName);
            else if (file.Extension.Equals(".hsg", StringComparison.InvariantCultureIgnoreCase))
                return new HelionSaveGameReader(file.FullName);

            return null;
        }

        private static string BuildWarpLegacy(string map)
        {
            List<string> numbers = new List<string>();
            StringBuilder num = new StringBuilder();

            for (int i = 0; i < map.Length; i++)
            {
                if (char.IsDigit(map[i]))
                {
                    num.Append(map[i]);
                }
                else
                {
                    if (num.Length > 0) numbers.Add(num.ToString());
                    num.Clear();
                }
            }

            if (num.Length > 0)
                numbers.Add(num.ToString());

            StringBuilder sb = new StringBuilder();
            foreach (string number in numbers)
            {
                sb.Append(Convert.ToInt32(number));
                sb.Append(' ');
            }

            if (numbers.Any())
                sb.Remove(sb.Length - 1, 1);

            return $" -warp {sb.ToString()}";
        }
    }
}
