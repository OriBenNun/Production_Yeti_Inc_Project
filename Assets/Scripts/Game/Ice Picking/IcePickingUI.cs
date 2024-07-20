using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IcePickingUI : MonoBehaviour
{
    [SerializeField] private IcePickingManager _manager;

    [SerializeField] private TMP_Text _iceCounter;
    [SerializeField] private RectTransform _tapPrompt;
    [SerializeField] private Image _helmetImage;

    private void OnEnable()
    {
        _manager.OnChop += UpdateOnChop;
        _manager.OnPlayerStart += OnFirstInput;
        _manager.OnPlayerHit += OnHit;
        _iceCounter.text = string.Empty;
        _tapPrompt.gameObject.SetActive(true);

        if(_manager.Defense > 0)
            _helmetImage.gameObject.SetActive(true);
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

    private void OnHit(IceBlock blockHit, bool isFatal)
    {
        if (_manager.Defense <= 0)
            _helmetImage.gameObject.SetActive(false);
    }
}
