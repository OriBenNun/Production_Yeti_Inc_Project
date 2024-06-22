using UnityEngine;
using UnityEngine.EventSystems;

public class IcePickingManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool _isLeft;


    public void OnPointerClick(PointerEventData eventData)
    {
        string side = _isLeft? "left" : "right";
        Debug.Log(eventData.position + $" on the {side} side.");
    }
}
