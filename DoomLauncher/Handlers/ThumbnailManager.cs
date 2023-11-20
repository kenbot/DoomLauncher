﻿using DoomLauncher.DataSources;
using DoomLauncher.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace DoomLauncher
{
    public static class ThumbnailManager
    {
        private static List<IGameFile> IWads = new List<IGameFile>();

        public static void SetIWads(List<IGameFile> iwads)
        {
            IWads = iwads;
        }

        public static void UpdateThumbnail(IGameFile gameFile)
        {
            bool delete = false;
            var screenshot = DataCache.Instance.DataSourceAdapter.GetFiles(gameFile, FileType.Screenshot).FirstOrDefault();
            var thumbnail = DataCache.Instance.DataSourceAdapter.GetFiles(gameFile, FileType.Thumbnail).FirstOrDefault();

            // All screenshots for this game file were deleted
            if (thumbnail != null && screenshot == null)
                delete = true;
            // The first screenshot was changed
            if (thumbnail != null && screenshot != null && thumbnail.SourcePortID != screenshot.FileID)
                delete = true;

            if (delete)
            {
                string file = Path.Combine(DataCache.Instance.AppConfiguration.ThumbnailDirectory.GetFullPath(), thumbnail.FileName);
                try
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }
                catch (IOException)
                {
                    // File is in use, insert to delete on next startup
                    DataCache.Instance.DataSourceAdapter.InsertCleanupFile(new CleanupFile() { FileName = file });
                }

                DataCache.Instance.DataSourceAdapter.DeleteFile(thumbnail);
            }

            GetOrCreateThumbnail(gameFile);
        }

        // Returns or creates a new thumbnail and inserts into database if it doesn't exist
        // Will search screenshots and thumbnails if provided, otherwise will check from database
        public static IFileData GetOrCreateThumbnail(IGameFile gameFile, IEnumerable<IFileData> screenshots = null, IEnumerable<IFileData> thumbnails = null,
            bool checkIWad = true)
        {
            if (thumbnails == null)
                thumbnails = DataCache.Instance.DataSourceAdapter.GetFiles(gameFile, FileType.Thumbnail);

            var thumbnail = thumbnails.FirstOrDefault(x => x.GameFileID == gameFile.GameFileID.Value);

            if (thumbnail != null)
                return thumbnail;

            if (screenshots == null)
                screenshots = DataCache.Instance.DataSourceAdapter.GetFiles(gameFile, FileType.Screenshot);

            var screenshot = screenshots.FirstOrDefault(x => x.GameFileID == gameFile.GameFileID.Value);
            if (screenshot != null)
            {
                string thumbnailFile = CreateThumbnail(screenshot);
                if (thumbnailFile == null)
                    return null;

                // Store the FileID of the screenshot in SourcePortID so we can use it to keep track of screenshot re-ordering / deletes
                FileData fileData = new FileData()
                {
                    GameFileID = gameFile.GameFileID.Value,
                    FileName = thumbnailFile,
                    FileTypeID = FileType.Thumbnail,
                    SourcePortID = screenshot.FileID.Value
                };

                DataCache.Instance.DataSourceAdapter.InsertFile(fileData);
                return fileData;
            }

            if (checkIWad && gameFile.IWadID.HasValue)
            {
                var iwad = IWads.FirstOrDefault(x => x.IWadID == gameFile.IWadID.Value);
                if (iwad != null)
                    return GetOrCreateThumbnail(iwad, checkIWad: false);
            }

            return null;
        }

        private static string CreateThumbnail(IFileData screenshot)
        {
            string file = Path.Combine(DataCache.Instance.AppConfiguration.ScreenshotDirectory.GetFullPath(), screenshot.FileName);
            if (!File.Exists(file))
                return null;

            using (Image image = Image.FromFile(file))
            {
                using (Image thumb = image.FixedSize(GameFileTile.ImageWidth, GameFileTile.ImageHeight, Color.Black))
                {
                    string filename = Guid.NewGuid().ToString() + ".png";
                    thumb.Save(Path.Combine(DataCache.Instance.AppConfiguration.ThumbnailDirectory.GetFullPath(), filename), ImageFormat.Png);
                    return filename;
                }
            }
        }
    }
}
