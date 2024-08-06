using UnityEngine;

public class IcePickingStarter : MonoBehaviour
{
    [SerializeField] private int defense = 1;
    [SerializeField] private IcePickingManager _manager;


    private void Start()
    {
        _manager.Init(defense);
    }
}
