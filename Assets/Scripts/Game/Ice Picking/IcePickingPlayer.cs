using UnityEngine;

public class IcePickingPlayer : MonoBehaviour
{
    [field: SerializeField] public Sides CurrentSide { get; private set; }

    [SerializeField] private IcePickingManager _icePickingManager;

    [SerializeField] private Transform _leftPosition;
    [SerializeField] private Transform _rightPosition;

    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _icePickingManager.OnChop += TriggerChopAnim;
    }

    private void OnDisable()
    {
        _icePickingManager.OnChop -= TriggerChopAnim;
    }

    public void SetCurrentSide(Sides side)
    {
        CurrentSide = side;
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        if(Sides.Left == CurrentSide)
        {
            _sprite.transform.position = _leftPosition.position;
            _sprite.flipX = false;
        }
        else
        {
            _sprite.transform.position = _rightPosition.position;
            _sprite.flipX = true;
        }
    }

    private void TriggerChopAnim()
    {
        _animator.SetTrigger("ChopTrigger");
    }

    private void OnValidate()
    {
        MoveCharacter();
    }
}
