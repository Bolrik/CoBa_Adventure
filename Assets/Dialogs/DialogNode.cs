using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Dialogs
{
    [System.Serializable]
    public class DialogNode
    {
        [SerializeField] private string speaker;
        public string Speaker { get { return speaker; } }

        [SerializeField] private string[] lines;
        public string[] Lines { get { return lines; } }

    }
}
