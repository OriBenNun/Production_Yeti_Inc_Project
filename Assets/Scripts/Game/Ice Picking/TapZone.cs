using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Ice_Picking
{
    public class TapZone : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Sides _side;
        [SerializeField] private IcePickingManager _icePickingManager;

        public void OnPointerClick(PointerEventData eventData)
        {
            _icePickingManager.ChopAction(_side);
        }
    }
}
