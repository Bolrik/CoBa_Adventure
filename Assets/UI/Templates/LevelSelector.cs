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



        private void Start()
        {
            IEnumerable<World> worlds = LevelManager.Instance.GetWorlds();

            int worldIndex = 0;
            Debug.Log("Loading Worlds...");

            foreach (var world in worlds)
            {
                Debug.Log($"World {worldIndex}");

                for (int levelIndex = 0; levelIndex < world.Count; levelIndex++)
                {
                    LevelButton levelButton = GameObject.Instantiate(this.LevelButtonPrefab);
                    levelButton.WorldIndex = worldIndex;
                    levelButton.LevelIndex = levelIndex;
                    levelButton.SetText($"{levelIndex + 1}");

                    levelButton.transform.SetParent(this.ButtonHost.transform, false);
                }

                worldIndex++;
            }

            //IEnumerable<int> levels = LevelManager.Instance.GetLevelIndices();

            //foreach (var levelIndex in levels)
            //{
            //    LevelButton levelButton = GameObject.Instantiate(this.LevelButtonPrefab);
            //    levelButton.Index = levelIndex;
            //    levelButton.SetText($"{levelIndex}");

            //    levelButton.transform.SetParent(this.ButtonHost.transform, false);
            //}
        }
    }
}
