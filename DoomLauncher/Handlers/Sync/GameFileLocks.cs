using DoomLauncher.DataSources;
using DoomLauncher.Interfaces;
using System.Collections.Concurrent;

namespace DoomLauncher.Handlers.Sync
{

    public interface IGameFileLocks
    {
        bool IsLocked(IGameFile gameFile);

        bool TryLock(IGameFile gameFile);

        void Unlock(IGameFile gameFile);
    }

    public class GameFileLocks : IGameFileLocks
    {
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
                return m_lockedGameFiles.TryAdd(gameFile.GameFileID.Value, 0);
            else
                return false;
        }

        public void Unlock(IGameFile gameFile)
        {
            if (gameFile.GameFileID.HasValue)
                m_lockedGameFiles.TryRemove(gameFile.GameFileID.Value, out _);
        }
    }
}
