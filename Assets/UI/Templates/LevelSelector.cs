using Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Buttons;
using UnityEngine;

namespace UI.Templates
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] private LevelButton levelButtonPrefab;
        public LevelButton LevelButtonPrefab { get { return levelButtonPrefab; } }

        [SerializeField] private GameObject buttonHost;
        public GameObject ButtonHost { get { return buttonHost; } }

        bool LockedLoad { get; set; }

        private void Start()
        {
            this.LoadLevels(false);
        }

        private void LoadLevels(bool includeLocked)
        {
            this.ButtonHost.transform.Clear();

            IEnumerable<World> worlds = LevelManager.Instance.GetWorlds();

            int worldIndex = 0;
            Debug.Log("Loading Worlds...");

            foreach (var world in worlds)
            {
                Debug.Log($"World {worldIndex}");

                for (int levelIndex = 0; levelIndex < world.Count; levelIndex++)
                {
                    Level.Level level = world.Get(levelIndex);

                    if (!includeLocked && !level.IsUnlocked)
                        continue;

                    LevelButton levelButton = GameObject.Instantiate(this.LevelButtonPrefab);
                    levelButton.WorldIndex = worldIndex;
                    levelButton.LevelIndex = levelIndex;
                    levelButton.SetText($"{levelIndex + 1}");

                    levelButton.transform.SetParent(this.ButtonHost.transform, false);
                }

                worldIndex++;
            }
        }

        private void Update()
        {
            if (!this.LockedLoad && Input.GetKeyDown(KeyCode.T))
            {
                this.LockedLoad = true;
                this.LoadLevels(true);
            }
        }
    }
}
