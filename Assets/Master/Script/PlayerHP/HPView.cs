using UnityEngine;
using UnityEngine.UI;

public class HPView : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider = default;
    [SerializeField] private Text _hpText = default;

    public void SetMaxHp(float maxHp)
    {
        _hpSlider.maxValue = maxHp;
    }

    // テキスト表示
    public void ShowHp(int hp)
    {
        _hpText.text = "HP: " + hp;
    }

    public void SetSliderValue(float value)
    {
        _hpSlider.value = value;
    }

    public void SetValueText(float value)
    {
        _hpText.text = value.ToString("F2");
    }
}