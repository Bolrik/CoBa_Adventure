using Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class ExitMenuButton : GameButton
    {
        public override void OnClick()
        {
            Application.Quit();
        }
    }
}