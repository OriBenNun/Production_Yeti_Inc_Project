using System;
using System.Collections;
using Pool_System;
using UnityEngine;

namespace Game.Water_Area
{
    public class Obstacle : PoolGameObject
    {
        [SerializeField] private float speed = 3f;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private bool _isMoving;
        public void Init(float turnOffDelay, Sprite randomObstacleSprite)
        {
            _isMoving = true;

            spriteRenderer.sprite = randomObstacleSprite;

            StartCoroutine(DisableAfterDelay(turnOffDelay));
        }

        private void FixedUpdate()
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
            transform.Translate(Vector2.down * (speed * Time.fixedDeltaTime));
        }
        
        private IEnumerator DisableAfterDelay(float turnOffDelay)
        {
            yield return new WaitForSeconds(turnOffDelay);
            
            Disable();
        }
    }
}
