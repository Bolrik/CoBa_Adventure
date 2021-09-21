using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Achievements
{
    [CreateAssetMenu(fileName = "Achievement", menuName = "Achievements/New Achievement")]
    public class AchievementData : ScriptableObject
    {
        [SerializeField] private string title;
        public string Title { get { return title; } }

        [SerializeField] private string desc;
        public string Desc { get { return desc; } }

        [SerializeField] private Sprite image;
        public Sprite Image { get { return image; } }


        [SerializeField] private bool previewDesc;
        public bool PreviewDesc { get { return previewDesc; } }


        [SerializeField] private AchievementType type;
        public AchievementType Type { get { return type; } }
    }
}