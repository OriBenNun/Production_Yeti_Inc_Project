using Pool_System;
using UnityEngine;
public class IceBlockPool : ObjectPool<IceBlock>
{
    [field: SerializeField] public int InitialPoolSize { get; private set; }
    [SerializeField] private IceBlock _iceBlockPrefab;
    [SerializeField] private Transform _parentTransform;

    public void Init()
    {
        InitPool(InitialPoolSize, _iceBlockPrefab, _parentTransform);
    }
}
