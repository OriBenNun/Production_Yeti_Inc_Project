using Game.Ice_Picking;
using System;
using UnityEngine;

namespace Game
{
    public static class CurrentRunDataHandler
    {
        public static event Action<int> OnCurrerntCurrencyChanged;


        public static int CurrentCurrency {  get; set; }

        private static int _currentLife;

        public static int CurrentLife
        {
            get { return _currentLife; }
            set { _currentLife = Mathf.Clamp(value, 0, MaxLife); }
        }

        public static int MaxLife => 4;
        public static bool HasHelmet { get; set; }
        public static bool HasRam { get; set; }
        public static bool HasNet { get; set; }
        
        
        public static bool HasInitialized { get; private set; }
        
        private const int _initialCurrency = 0;
        private const int _initialLife = 4;


        public static void Init()
        {
            CurrentCurrency = _initialCurrency;
            CurrentLife = _initialLife;

            HasHelmet = false;
            HasRam = false;
            HasNet = false;
            
            HasInitialized = true;

            OnCurrerntCurrencyChanged?.Invoke(CurrentCurrency);
            IcePickingCompletionManager.OnIcePickingComplete += HandleOnIcePickingComplete;
        
        }

        private static void HandleOnIcePickingComplete(int iceCubes)
        {
            CurrentCurrency += iceCubes;
            OnCurrerntCurrencyChanged?.Invoke(CurrentCurrency);
        }

        public static void Reset()
        {
            HasInitialized = false;

            IcePickingCompletionManager.OnIcePickingComplete -= HandleOnIcePickingComplete;
        }
    }
}
