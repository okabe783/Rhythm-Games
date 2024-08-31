using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary> コンボ数をテキストに反映 </summary>
public class ComboManager : MonoBehaviour
{
    [SerializeField, Header("コンボのキャンバス")] private Text _comboText;
    [SerializeField, Header("コンボテキスト")] private Text _combo;
    [SerializeField, Header("ComboのUI")] private GameObject _comboUI;
    
    public void SetCombo(string text)
    {
        _comboText.text = text;
    }

    public void ComboVisibility(bool showCombo)
    {
        _comboUI.SetActive(showCombo);
        _combo.gameObject.SetActive(showCombo);
        _comboText.gameObject.SetActive(showCombo);
    }
}
