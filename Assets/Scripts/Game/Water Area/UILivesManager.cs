using System.Collections.Generic;
using UnityEngine;

namespace Game.Water_Area
{
    public class UILivesManager : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> livesParents;

        private void Awake()
        {
            CurrentRunDataHandler.OnCurrentLifeChanged += UpdateLives;
        }
        
        private void Start()
        {
            UpdateLives(CurrentRunDataHandler.CurrentLife);
        }
    
        private void OnDestroy()
        {
            CurrentRunDataHandler.OnCurrentLifeChanged -= UpdateLives;
        }

        private void UpdateLives(int lives)
        {
            for (var i = 0; i < livesParents.Count; i++)
            {
                livesParents[i].gameObject.SetActive(i < lives);
            }
        }
    }
}
