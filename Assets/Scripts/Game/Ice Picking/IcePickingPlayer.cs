using UnityEngine;

public class IcePickingPlayer : MonoBehaviour
{
    [field: SerializeField] public Sides CurrentSide { get; private set; }

    [SerializeField] private Transform _leftPosition;
    [SerializeField] private Transform _rightPosition;

    [SerializeField] private SpriteRenderer _sprite;

    public void SetCurrentSide(Sides side)
    {
        CurrentSide = side;
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        if(Sides.Left == CurrentSide)
            _sprite.transform.position = _leftPosition.position;
        else
            _sprite.transform.position = _rightPosition.position;
    }

    private void OnValidate()
    {
        MoveCharacter();
    }
}
