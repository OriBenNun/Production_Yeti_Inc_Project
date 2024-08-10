using System.Collections;
using Pool_System;
using UnityEngine;

namespace Game.Water_Area.Obstacles
{
    public class Obstacle : PoolGameObject
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private float _speed;
        private bool _isMoving;
        public void Init(float turnOffDelay, Sprite randomObstacleSprite, float speed)
        {
            _isMoving = true;
            _speed = speed;
            
            spriteRenderer.sprite = randomObstacleSprite;

            StartCoroutine(DisableAfterDelay(turnOffDelay));
        }

        private void Update()
        {
            if (!_isMoving) { return; }

            MoveDown();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _isMoving = false;
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        private void MoveDown()
        {
            transform.Translate(Vector2.down * (_speed * Time.deltaTime));
        }
        
        private IEnumerator DisableAfterDelay(float turnOffDelay)
        {
            yield return new WaitForSeconds(turnOffDelay);
            
            Disable();
        }
    }
}
