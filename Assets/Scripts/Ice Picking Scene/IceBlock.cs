using UnityEngine;



public class IceBlock : MonoBehaviour
{
    public enum IceBlockVariation
    {
        NoSpike,
        LeftSpike,
        RightSpike,
    }

    [SerializeField] private GameObject _leftSpike;
    [SerializeField] private GameObject _rightSpike;
    [field: SerializeField] public IceBlockVariation Variation { get; private set; }

    public void Init(IceBlockVariation variation)
    {
        Variation = variation;
        SetSpikes();
    }


    public void Clean()
    {
        Variation = IceBlockVariation.NoSpike;
    }


    private void SetSpikes()
    {
        switch (Variation)
        {
            case IceBlockVariation.NoSpike:
                _leftSpike.SetActive(false);
                _rightSpike.SetActive(false);
                break;

            case IceBlockVariation.LeftSpike:
                _leftSpike.SetActive(true);
                _rightSpike.SetActive(false);
                break;

            case IceBlockVariation.RightSpike:
                _leftSpike.SetActive(false);
                _rightSpike.SetActive(true);
                break;

            default:
                _leftSpike.SetActive(false);
                _rightSpike.SetActive(false);
                break;
        }
    }
}
