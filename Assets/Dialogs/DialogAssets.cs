using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogs
{
    public class DialogAssets : MonoBehaviour
    {
        #region Singleton Pattern		
        static DialogAssets Instance { get; set; }

        private void Awake()
        {
            if (Instance != null)
            {
                if (Instance == this)
                    return;

                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion


        [SerializeField] private Dialog[] dialogs;
        public static Dialog[] Dialogs { get { return Instance.dialogs; } }

    }
}
