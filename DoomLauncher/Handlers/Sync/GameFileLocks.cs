using DoomLauncher.DataSources;
using DoomLauncher.Interfaces;
using System.Collections.Concurrent;

namespace DoomLauncher.Handlers.Sync
{

    public delegate void GameFileLockHandler(int gameFileID);

    public interface IGameFileLocks
    {
        event GameFileLockHandler GameFileLocked;
        event GameFileLockHandler GameFileUnlocked;

        bool IsLocked(IGameFile gameFile);

        bool TryLock(IGameFile gameFile);

        void Unlock(IGameFile gameFile);
    }

    public class GameFileLocks : IGameFileLocks
    {
        public event GameFileLockHandler GameFileLocked;
        public event GameFileLockHandler GameFileUnlocked;

        private ConcurrentDictionary<int, byte> m_lockedGameFiles = new ConcurrentDictionary<int, byte>();


        public bool IsLocked(IGameFile gameFile)
        {
            if (gameFile.GameFileID.HasValue)
                return m_lockedGameFiles.ContainsKey(gameFile.GameFileID.Value);
            else
                return false;
        }

        public bool TryLock(IGameFile gameFile)
        {
            if (gameFile.GameFileID.HasValue)
            {
                int gameFileId = gameFile.GameFileID.Value;
                var lockSucceeded = m_lockedGameFiles.TryAdd(gameFileId, 0);
                if (lockSucceeded && GameFileLocked != null)
                    GameFileLocked.Invoke(gameFileId);
                return lockSucceeded;
            }
            else
                return false;
        }

        public void Unlock(IGameFile gameFile)
        {
            if (gameFile.GameFileID.HasValue)
            {
                int gameFileId = gameFile.GameFileID.Value;
                var unlockSucceeded = m_lockedGameFiles.TryRemove(gameFileId, out _);
                if (unlockSucceeded && GameFileUnlocked != null)
                    GameFileUnlocked.Invoke(gameFileId);
            }
        }
    }
}
