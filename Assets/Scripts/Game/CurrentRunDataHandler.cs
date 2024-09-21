using Game.Ice_Picking;
using System;
using Game.Water_Area;
using UnityEngine;

namespace Game
{
    public static class CurrentRunDataHandler
    {
        public static event Action<int> OnCurrentCurrencyChanged;
        public static event Action<int> OnCurrentLifeChanged;
        public static event Action OnBoughtItemsChanged;

        public static int CurrentCurrency { get; set; }
        public static int TotalCurrencyInRun { get; set; }

        private static int _currentLife;

        public static int CurrentLife
        {
            get => _currentLife;
            set => _currentLife = Mathf.Clamp(value, 0, MaxLife);
        }

        public static int DistanceTraveled { get; set; }
        public static float CurrentRunTime { get; set; }

        public static int MaxLife => 4;
        public static bool HasHelmet { get; set; }
        public static bool HasRam { get; set; }
        public static bool HasNet { get; set; }

        public static bool HasInitialized { get; private set; }

        private const int _initialLife = 4;
        private const int _currencyToLoseOnObstacleHit = 20;

        public static void Init()
        {
            CurrentCurrency = 1000;
            TotalCurrencyInRun = 0;
            CurrentLife = _initialLife;
            DistanceTraveled = 0;
            CurrentRunTime = 0;

            HasHelmet = false;
            HasRam = false;
            HasNet = false;

            HasInitialized = true;

            OnCurrentCurrencyChanged?.Invoke(CurrentCurrency);
            IcePickingCompletionManager.OnIcePickingComplete += HandleOnIcePickingComplete;

            WaterPlayer.OnPlayerGotHit += HandleOnPlayerGotHit;
        }

        public static void Reset()
        {
            HasInitialized = false;

            IcePickingCompletionManager.OnIcePickingComplete -= HandleOnIcePickingComplete;
            WaterPlayer.OnPlayerGotHit -= HandleOnPlayerGotHit;
        }

        private static void HandleOnIcePickingComplete(int iceCubes)
        {
            TotalCurrencyInRun += iceCubes;
            CurrentCurrency += iceCubes;
            OnCurrentCurrencyChanged?.Invoke(CurrentCurrency);
        }

        private static void HandleOnPlayerGotHit()
        {
            if (HasRam)
            {
                HasRam = false;
                OnBoughtItemsChanged?.Invoke();
            }
            else
            {
                CurrentLife = Math.Max(CurrentLife - 1, 0);
                OnCurrentLifeChanged?.Invoke(CurrentLife);
            }

            if (CurrentLife == 0) return;
            
            if (HasNet)
            {
                HasNet = false;
                OnBoughtItemsChanged?.Invoke();
            }
            else
            {
                CurrentCurrency = Math.Max(CurrentCurrency - _currencyToLoseOnObstacleHit, 0);
                OnCurrentCurrencyChanged?.Invoke(CurrentCurrency);
            }
        }
    }
}