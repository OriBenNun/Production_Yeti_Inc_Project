using System;
using UnityEngine;

namespace Game.Water_Area
{
    public class CollectibleIceCube : MonoBehaviour
    {
        private float _speed;

        public void Init(int timeToLive, float speed, Vector2 spawnPosition)
        {
            _speed = speed;
            transform.position = spawnPosition;
            Destroy(gameObject, timeToLive);
        }
        
        private void Update()
        {
            MoveDown();
        }

        private void MoveDown()
        {
            transform.Translate(Vector2.down * (_speed * Time.deltaTime));
        }
    }
}
