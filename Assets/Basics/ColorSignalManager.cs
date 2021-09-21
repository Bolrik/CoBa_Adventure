using Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Basics
{
    public class ColorSignalManager
    {
        #region Singleton Pattern
        public static ColorSignalManager Instance { get; private set; }
        static ColorSignalManager()
        {
            new ColorSignalManager();
        }

        private ColorSignalManager()
        {
            Instance = this;

            this.Initialize();

            LevelManager.Instance.PreLoadLevel += this.ReInitialize;
        }
        #endregion

        Dictionary<ColorCode, List<IColorSignalReceiver>> Receivers { get; set; } = new Dictionary<ColorCode, List<IColorSignalReceiver>>();

        private void Initialize()
        {
            //this.Signals = new Dictionary<ColorCode, bool>();

            //for (int idx = 1; idx <= 3; idx++)
            //{
            //    this.Signals.Add((ColorCode)idx, false);
            //}
        }


        private void RegisterSignalReceiver(IColorSignalReceiver signalReceiver)
        {
            ColorCode colorCode = signalReceiver.ColorCode;

            if (!this.Receivers.ContainsKey(colorCode))
                this.Receivers.Add(colorCode, new List<IColorSignalReceiver>());

            this.Receivers[colorCode].Add(signalReceiver);
        }

        private void UnregisterSignalReceiver(IColorSignalReceiver signalReceiver)
        {
            ColorCode colorCode = signalReceiver.ColorCode;

            if (!this.Receivers.ContainsKey(colorCode))
                return;

            this.Receivers[colorCode].Remove(signalReceiver);
        }

        private void SendColorSignal(ColorCode colorCode)
        {
            if (!this.Receivers.ContainsKey(colorCode))
                return;

            //bool signal = this.Signals[colorCode];

            foreach (var signalReceiver in this.Receivers[colorCode])
            {
                // signalReceiver.UpdateSignal(colorCode, signal);
                signalReceiver.UpdateSignal(colorCode);
            }
        }

        void ReInitialize()
        {
            this.Receivers.Clear();
        }


        #region Statics
        public static void Register(IColorSignalReceiver signalReceiver)
        {
            Instance.RegisterSignalReceiver(signalReceiver);
        }
        public static void Unregister(IColorSignalReceiver signalReceiver)
        {
            Instance.UnregisterSignalReceiver(signalReceiver);
        }

        public static void SendSignal(ColorCode colorCode)
        {
            Instance.SendColorSignal(colorCode);
        }


        #endregion
    }

    public interface IColorSignalReceiver
    {
        ColorCode ColorCode { get; }
        void UpdateSignal(ColorCode colorCode);
    }
}