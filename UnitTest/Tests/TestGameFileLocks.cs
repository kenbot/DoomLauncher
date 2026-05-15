using DoomLauncher.DataSources;
using DoomLauncher.Handlers.Sync;
using DoomLauncher.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Tests
{
    [TestClass]
    public class TestGameFileLocks
    {
        [TestMethod]
        public void LockUnlockedGameFileSucceeds()
        {
            IGameFile gameFile = new GameFile { GameFileID = 114 };
            IGameFileLocks locks = new GameFileLocks();
            Assert.IsFalse(locks.IsLocked(gameFile));

            var success = locks.TryLock(gameFile);

            Assert.IsTrue(success);
            Assert.IsTrue(locks.IsLocked(gameFile));
        }

        [TestMethod]
        public void LockLockedGameFileFails()
        {
            IGameFile gameFile = new GameFile { GameFileID = 114 };
            IGameFileLocks locks = new GameFileLocks();
            Assert.IsFalse(locks.IsLocked(gameFile));

            var success = locks.TryLock(gameFile);
            Assert.IsTrue(success);

            var success2 = locks.TryLock(gameFile);
            Assert.IsFalse(success2);
            Assert.IsTrue(locks.IsLocked(gameFile));
        }

        [TestMethod]
        public void UnlockUnlocksLockedGameFile()
        {
            IGameFile gameFile = new GameFile { GameFileID = 114 };
            IGameFileLocks locks = new GameFileLocks();
            var success = locks.TryLock(gameFile);
            Assert.IsTrue(success);
            Assert.IsTrue(locks.IsLocked(gameFile));

            locks.Unlock(gameFile);

            Assert.IsFalse(locks.IsLocked(gameFile));
        }

        [TestMethod]
        public void UnlockUnlocksOnlyOneLockedGameFile()
        {
            IGameFile gameFile1 = new GameFile { GameFileID = 114 };
            IGameFile gameFile2 = new GameFile { GameFileID = 115 };

            IGameFileLocks locks = new GameFileLocks();

            var success1 = locks.TryLock(gameFile1);
            Assert.IsTrue(success1);
            Assert.IsTrue(locks.IsLocked(gameFile1));

            var success2 = locks.TryLock(gameFile2);
            Assert.IsTrue(success2);
            Assert.IsTrue(locks.IsLocked(gameFile2));

            locks.Unlock(gameFile1);

            Assert.IsFalse(locks.IsLocked(gameFile1));
            Assert.IsTrue(locks.IsLocked(gameFile2));
        }
    }
}
