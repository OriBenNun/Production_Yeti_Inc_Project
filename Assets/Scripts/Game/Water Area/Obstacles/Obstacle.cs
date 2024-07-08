using System;
using System.Collections;
using Pool_System;
using UnityEngine;

namespace Game.Water_Area
{
    public class Obstacle : PoolGameObject
    {
        [SerializeField] private float speed = 3f;
        
        private bool _isMoving;
        public void Init(float turnOffDelay)
        {
            _isMoving = true;

            StartCoroutine(TurnOffSelfAfterDelay(turnOffDelay));
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
        
        private void MoveDown()
        {
            transform.Translate(Vector2.down * (speed * Time.deltaTime));
        }
        
        private IEnumerator TurnOffSelfAfterDelay(float turnOffDelay)
        {
            yield return new WaitForSeconds(turnOffDelay);
            
            gameObject.SetActive(false);
        }
    }
}
