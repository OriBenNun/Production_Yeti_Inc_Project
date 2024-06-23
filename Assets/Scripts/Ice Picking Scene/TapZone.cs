using UnityEngine;
using UnityEngine.EventSystems;

public class TapZone : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sides _side;
    [SerializeField] private IcePickingManager _icePickingManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        _icePickingManager.Chop(_side);
        Debug.Log($"Tapped {_side} side");
    }
}
