using System;
using UnityEngine;

namespace Pool_System
{
    public abstract class PoolGameObject : MonoBehaviour
    {
        [SerializeField] private bool resetTransformOnClean = true;
        public event Action<PoolGameObject> OnObjectCanBeCleaned;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public virtual void ResetSelf()
        {
            if (!resetTransformOnClean) return;
            
            var myTransform = transform;
            myTransform.position = Vector3.zero;
            myTransform.rotation = Quaternion.identity;
            myTransform.localScale = Vector3.one;
        }
        
        protected virtual void OnDisable()
        {
            OnObjectCanBeCleaned?.Invoke(this);
        }
    }
}