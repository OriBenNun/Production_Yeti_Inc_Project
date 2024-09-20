using System;
using UnityEngine;

namespace Game
{
    public class 
        TimePlayedCounterSingleton : MonoBehaviour
    {
        public static TimePlayedCounterSingleton Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        private void Update()
        {
            if (Time.timeScale == 0) return;

            if (!CurrentRunDataHandler.HasInitialized) return;
            
            
            CurrentRunDataHandler.CurrentRunTime += Time.deltaTime;
        }
    }
}
