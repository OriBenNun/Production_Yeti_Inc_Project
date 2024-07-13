using TMPro;
using UnityEngine;

public class IcePickingUI : MonoBehaviour
{
    [SerializeField] private IcePickingManager _manager;

    [SerializeField] private TMP_Text _iceCounter;
    [SerializeField] private RectTransform _tapPrompt;


    private void OnEnable()
    {
        _manager.OnChop += UpdateOnChop;
        _manager.OnPlayerStart += OnFirstInput;
        _iceCounter.text = string.Empty;
        _tapPrompt.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _manager.OnChop -= UpdateOnChop;
        _manager.OnPlayerStart -= OnFirstInput;

    }

    private void UpdateOnChop()
    {
        _iceCounter.text = _manager.IceChopped.ToString();
    }

    private void OnFirstInput()
    {
        _iceCounter.text = "0";
        _tapPrompt.gameObject.SetActive(false);
    }
}
