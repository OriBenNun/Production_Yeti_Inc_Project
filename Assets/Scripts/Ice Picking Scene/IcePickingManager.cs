using UnityEngine;


public enum Sides
{
    Left,
    Right
}

public class IcePickingManager : MonoBehaviour
{
    [SerializeField] private IcePickingPlayer _player;

    [Header("Settings")]
    [SerializeField] private Sides _startingSide;

    public void Init()
    {
        _player.SetCurrentSide(_startingSide);
    }

    public void Chop(Sides side)
    {
        // Move the character to the chop side,
        _player.SetCurrentSide(side);
        
        // Chop Pillar
        // Check if hit by spike (check if the next block has a spike on current side

        
    }



}
