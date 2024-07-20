using System;
using UnityEngine;

namespace Game.Ice_Picking
{
    public class IcePickingCompletionManager : MonoBehaviour
    {
        [SerializeField] private IcePickingManager icePickingManager;
        
        public static event Action<int> OnIcePickingComplete;

        private void Awake()
        {
            icePickingManager.OnPlayerGotHit += OnPlayerHit;
            icePickingManager.OnTimerEnd += OnTimerEnd;
        }

        private void OnDestroy()
        {
            icePickingManager.OnPlayerGotHit -= OnPlayerHit;
            icePickingManager.OnTimerEnd -= OnTimerEnd;
        }
        
        private void OnTimerEnd()
        {
            EndIceChoppingMiniGame();
        }

        private void OnPlayerHit(IceBlock _, bool isKnockedOut)
        {
            if (!isKnockedOut)
            {
                return;
            }

            EndIceChoppingMiniGame();
        }

        private void EndIceChoppingMiniGame()
        {
            OnIcePickingComplete?.Invoke(icePickingManager.IceChopped);
            
            SceneTransitionHandler.LoadGameSceneAsync();
        }
    }
}
