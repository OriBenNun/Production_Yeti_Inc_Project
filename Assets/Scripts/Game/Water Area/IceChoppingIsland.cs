using System;
using System.Collections;
using UnityEngine;

namespace Game.Water_Area
{
    public class IceChoppingIsland : MonoBehaviour
    {

        public static event Action<IceChoppingIsland> OnIslandStopped;
        public void Init(float speed,float yPositionToStop)
        {
            StartCoroutine(MoveDownwardsUntilStop(speed, yPositionToStop));
        }

        private IEnumerator MoveDownwardsUntilStop(float speed, float yPositionToStop)
        {
            while (transform.position.y > yPositionToStop)
            {
                transform.Translate(Vector3.down * (speed * Time.deltaTime));
                yield return null;
            }
            
            OnIslandStopped?.Invoke(this);
        }
    }
}
