using UnityEngine;

public class IcePickingStarter : MonoBehaviour
{
    [SerializeField] private IcePickingManager _manager;


    private void Start()
    {
        _manager.Init();
    }
}
