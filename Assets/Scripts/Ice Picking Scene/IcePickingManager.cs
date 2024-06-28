using System.Collections.Generic;
using UnityEngine;
using static IceBlock;


public enum Sides
{
    Left,
    Right
}

public class IcePickingManager : MonoBehaviour
{
    [SerializeField] private IcePickingPlayer _player;
    [SerializeField] private IceBlockPool _iceBlockPool;

    [Header("Settings")]
    [SerializeField] private Sides _startingSide;
    [SerializeField] private Transform _pillarBasePoint;
    [SerializeField] private float _blockHeight;

    private Queue<IceBlock> _icePillarQueue;
    public void Init()
    {
        _iceBlockPool.Init();
        _player.SetCurrentSide(_startingSide);

        InitPillar();
    }

    public void ChopAction(Sides side)
    {
        // Move the character to the chop side,
        _player.SetCurrentSide(side);

        // ChopAction Pillar
        ChopPillar();

        // Particle Effect

        // Check if hit by spike (check if the next block has a spike on current side

    }

    
    private void InitPillar()
    {
        _icePillarQueue = new Queue<IceBlock>();

        for(int i = 0; i < _iceBlockPool.InitialPoolSize - 5; i++)
        {
            AddIceBlock();
        }
    }

    private void AddIceBlock()
    {
        IceBlock block = _iceBlockPool.GetReadyObject();
        Vector3 position = _pillarBasePoint.position;
        position.y += _blockHeight * _icePillarQueue.Count;
        block.Init(position, DetermineSpikeVariation());

        _icePillarQueue.Enqueue(block);
    }

    private void ChopPillar()
    {

        // Dequeue and disable the block chopped.
        IceBlock blockChopped = _icePillarQueue.Dequeue();
        blockChopped.gameObject.SetActive(false);

        // Lower Pillar
        foreach(var block in _icePillarQueue)
        {
            Vector3 position = block.transform.position;
            position.y -= _blockHeight;
            block.transform.position = position;
        }

        AddIceBlock();

    }
    private IceBlockVariation DetermineSpikeVariation()
    {
        return (IceBlockVariation)Random.Range(0, 3);
    }
   



}
