using System;
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
    public event Action<IceBlock> OnPlayerHit;



    [SerializeField] private IcePickingPlayer _player;
    [SerializeField] private IceBlockPool _iceBlockPool;

    [Header("Settings")]
    [SerializeField] private Sides _startingSide;
    [SerializeField] private Transform _pillarBasePoint;
    [SerializeField] private float _blockHeight;

    [Header("Pillar Generation Settings")]
    [SerializeField] private int _maxSpaceBetweenSpikes;
    [SerializeField] private int _maxSpikesBeforeSwitch;
    [SerializeField] private int _firstSpikeHeight = 4;



    private System.Random _random;
    private int _blocksSinceLastSpike;
    private int _spikesSinceLastSwap;
    private Sides _currentSpikeSide;
    private Queue<IceBlock> _icePillarQueue;
    public void Init()
    {
        _iceBlockPool.Init();
        _player.SetCurrentSide(_startingSide);
        _random = new System.Random();
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
        IceBlockVariation variation = _icePillarQueue.Peek().Variation;
        bool rightHit = (side == Sides.Right) && (variation == IceBlockVariation.RightSpike);
        bool leftHit = (side == Sides.Left) && (variation == IceBlockVariation.LeftSpike);
        if (rightHit || leftHit)
            PlayerHit();

    }

    private void PlayerHit()
    {
        OnPlayerHit?.Invoke(_icePillarQueue.Peek());

        Debug.Log("Player Got Hit");
    }
    private void InitPillar()
    {
        _blocksSinceLastSpike = _firstSpikeHeight - 1;
        _spikesSinceLastSwap = 0;
        _currentSpikeSide = UnityEngine.Random.Range(0,2) == 0 ? Sides.Left : Sides.Right;

        _icePillarQueue = new Queue<IceBlock>();

        for(int i = 0; i < _iceBlockPool.InitialPoolSize; i++)
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
        // The first X blocks have no spikes.
        if (_icePillarQueue.Count < _firstSpikeHeight)
            return IceBlockVariation.NoSpike;

        // Check if need to make a spike. 
        double chanceForSpike = (1f / (_maxSpaceBetweenSpikes - 1f)) * _blocksSinceLastSpike;
        double rollForSpike = _random.NextDouble();
        
        if (chanceForSpike <= rollForSpike)
        {
            _blocksSinceLastSpike++;
            return IceBlockVariation.NoSpike;
        }

        _blocksSinceLastSpike = 0;

        // Check if spikes need to switch side.
        double chanceForSwitch = (1f / _maxSpikesBeforeSwitch) * _spikesSinceLastSwap;
        double rollForSwap = _random.NextDouble();

        if (chanceForSwitch <= rollForSwap)
        {
            _spikesSinceLastSwap++;
        }
        else
        {
            _currentSpikeSide = _currentSpikeSide == Sides.Left ? Sides.Right : Sides.Left;
            _spikesSinceLastSwap = 1;
        }

        if (_currentSpikeSide == Sides.Left)
            return IceBlockVariation.LeftSpike;
        else
            return IceBlockVariation.RightSpike;
    }
   



}
