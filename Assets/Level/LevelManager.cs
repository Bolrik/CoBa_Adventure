using Basics;
using PlayerInteraction;
using PlayerInteraction.Interactives;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelManager
    {
        #region Singleton Pattern
        public static LevelManager Instance { get; private set; }
        static LevelManager()
        {
            new LevelManager();
        }

        private LevelManager()
        {
            Instance = this;

            this.Initialize();
        }
        #endregion

        List<Level> Levels { get; set; } = new List<Level>();
        public Level Level { get; private set; }
        public int LevelIndex { get; private set; }

        List<World> Worlds { get; set; } = new List<World>();
        public World World { get; private set; }
        public int WorldIndex { get; private set; }

        private NextLevel NextLevel { get; set; }



        // public int CurrentIndex { get; private set; } = 0;
        // public Level Current { get; private set; }

        LevelLayout Layout { get; set; }

        // Occures right before a new Level is loading.
        public Action PreLoadLevel { get; set; }
        // Occures right after a new level was loaded.
        public Action PostLoadLevel { get; set; }
        // Occures when the player completed the current level.
        public Action<LevelDoneEventArgs> LevelDone { get; set; }
        // Occures right before the level is restarted
        public Action RestartLevel { get; set; }


        public bool IsLevelDone { get; private set; }


        private void Initialize()
        {
            this.Worlds.Add(new World_001());
            this.Worlds.Add(new World_002());
        }


        //public void Next()
        //{
        //    int idx = this.CurrentIndex + 1;

        //    if (idx >= this.Levels.Count)
        //    {
        //        this.BackToTitleScreen();

        //        return;
        //    }

        //    this.Load(idx);
        //}

        public void Next()
        {
            int worldIndex = this.WorldIndex;
            int levelIndex = this.LevelIndex + 1;

            if (this.NextLevel != null)
            {
                worldIndex = this.NextLevel.WorldIndex;
                levelIndex = this.NextLevel.LevelIndex;

                this.NextLevel = null;
            }

            World world = this.World;
            if (this.WorldIndex != worldIndex)
                world = this.GetWorld(worldIndex);

            if (world == null || world.Get(levelIndex) == null)
            {
                this.BackToTitleScreen();

                return;
            }

            this.Load(worldIndex, levelIndex);
        }

        //public void BackToTitleScreen()
        //{
        //    this.Current = null;
        //    this.CurrentIndex = -1;

        //    this.PreLoadLevel?.Invoke();
        //    SceneManager.LoadScene(1);
        //    this.PostLoadLevel?.Invoke();
        //}

        public void BackToTitleScreen()
        {
            this.World = null;
            this.Level = null;

            this.WorldIndex = -1;
            this.LevelIndex = -1;

            this.PreLoadLevel?.Invoke();
            SceneManager.LoadScene(1);
            this.PostLoadLevel?.Invoke();
        }

        //public void Load(int index)
        //{
        //    this.IsLevelDone = false;

        //    if (this.Layout != null)
        //    {
        //        this.Layout.Destroy();
        //        this.Layout = null;
        //    }

        //    if (this.SwapScene())
        //    {
        //        this.CurrentIndex = index;
        //        return;
        //    }

        //    this.PreLoadLevel?.Invoke();

        //    this.CurrentIndex = index;

        //    if (this.CurrentIndex >= this.Levels.Count)
        //    {
        //        this.Current = null;
        //        return;
        //    }

        //    this.Current = this.Levels.ElementAt(this.CurrentIndex);
        //    this.Layout = this.Current.Load();

        //    this.PostLoadLevel?.Invoke();

        //    this.Current.PostLoadActions();
        //}

        public void Load(int worldIndex, int levelIndex)
        {
            this.IsLevelDone = false;

            if (this.Layout != null)
            {
                this.Layout.Destroy();
                this.Layout = null;
            }

            if (this.SwapScene())
            {
                this.WorldIndex = worldIndex;
                this.LevelIndex = levelIndex;
                return;
            }

            this.PreLoadLevel?.Invoke();

            this.WorldIndex = worldIndex;
            this.LevelIndex = levelIndex;

            this.World = this.GetWorld(this.WorldIndex);

            if (this.World == null)
                return;

            this.Level = this.World.Get(this.LevelIndex);

            this.Layout = this.Level.Load();

            this.PostLoadLevel?.Invoke();

            this.Level.PostLoadActions();
        }

        public void Reload()
        {
            this.Load(this.WorldIndex, this.LevelIndex);
            this.RestartLevel?.Invoke();
        }

        private bool SwapScene()
        {
            Scene scene = SceneManager.GetActiveScene();

            if (scene.buildIndex != 2)
            {
                SceneManager.sceneLoaded += this.SceneManager_SceneLoaded;

                SceneManager.LoadScene(2);

                return true;
            }

            return false;
        }

        private void SceneManager_SceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            this.Reload();
            SceneManager.sceneLoaded -= this.SceneManager_SceneLoaded;
        }


        public IEnumerable<World> GetWorlds()
        {
            for (int idx = 0; idx < this.Worlds.Count; idx++)
            {
                yield return this.Worlds[idx];
            }
        }

        private World GetWorld(int idx)
        {
            if (idx < 0 || this.Worlds.Count <= idx)
            {
                return null;
            }

            return this.Worlds[idx];
        }


        public void MarkAsDone(GoalFlag goalFlag, ColorBall colorBall)
        {
            if (this.IsLevelDone)
                return;

            this.IsLevelDone = true;

            if (this.Unlock(goalFlag))
            {
                this.NextLevel = new NextLevel(goalFlag.WorldIndex, goalFlag.LevelIndex);
            }
            else
                this.Unlock(this.WorldIndex, this.LevelIndex + 1);

            string victoryText = this.Level.GetVictoryText();
            LevelTransitionManager.Instance.SetTransitionText(victoryText);

            this.LevelDone?.Invoke(new LevelDoneEventArgs(colorBall));
        }

        private bool Unlock(GoalFlag goalFlag)
        {
            return this.Unlock(goalFlag.WorldIndex, goalFlag.LevelIndex);
        }

        private bool Unlock(int worldIndex, int levelIndex)
        {
            World world = this.GetWorld(worldIndex);

            if (world != null)
            {
                Level level = world.Get(levelIndex);

                if (level != null)
                {
                    level.SetUnlocked(true);
                    return true;
                }
            }

            return false;
        }
    }

    public class LevelDoneEventArgs
    {
        public ColorBall FinishedBy { get; set; }

        public LevelDoneEventArgs(ColorBall finishedBy)
        {
            this.FinishedBy = finishedBy;
        }
    }

    public class NextLevel
    {
        public int WorldIndex { get; private set; } = -1;
        public int LevelIndex { get; private set; } = -1;

        public NextLevel(int worldIndex, int levelIndex)
        {
            this.WorldIndex = worldIndex;
            this.LevelIndex = levelIndex;
        }
    }
}
