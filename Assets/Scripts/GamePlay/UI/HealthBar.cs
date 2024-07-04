using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _image;
    private Unit _unit;

    public void Init(Unit unit)
    {
        _unit = unit;
        _unit.OnHealthChanged += UpdateUI;
    }

    private void UpdateUI()
    {
        _image.fillAmount = (float)_unit.Health / (float)Unit.MAX_HEALTH;
    }
}
