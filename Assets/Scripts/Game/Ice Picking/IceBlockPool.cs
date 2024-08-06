using System.Collections.Generic;
using Pool_System;
using UnityEngine;

namespace Game.Ice_Picking
{
    public class IceBlockPool : ObjectPool<IceBlock>
    {
        [SerializeField] private List<Sprite> _iceBlockSprites;

        [field: SerializeField] public int InitialPoolSize { get; private set; }
        [SerializeField] private IceBlock _iceBlockPrefab;
        [SerializeField] private Transform _parentTransform;

        public void Init()
        {
            InitPool(InitialPoolSize, _iceBlockPrefab, _parentTransform);
        }

        public Sprite GetRandomBlockSprite()
        {
            int rnd = Random.Range(0, _iceBlockSprites.Count);
            return _iceBlockSprites[rnd];
        }
    }
}
