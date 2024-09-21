using UnityEngine;

namespace Game.Ice_Picking
{
    public class IcePickingStarter : MonoBehaviour
    {
        [SerializeField] private IcePickingManager _manager;
        [SerializeField] private IcePickingUI _ui;


        private void Start()
        {
            _manager.Init(CurrentRunDataHandler.HasHelmet? 1 : 0);
            _ui.Init();
        }
    }
}
