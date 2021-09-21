using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Dialogs
{
    [CreateAssetMenu(fileName = "New Dialog", menuName = "Dialogs/New Dialog")]
    public class Dialog : ScriptableObject
    {
        [SerializeField] private int worldIndex;
        public int WorldIndex { get { return worldIndex; } }

        [SerializeField] private int levelIndex;
        public int LevelIndex { get { return levelIndex; } }

        [SerializeField] private DialogNode[] dialogNodes;
        public DialogNode[] DialogNodes { get { return dialogNodes; } }
    }
}
