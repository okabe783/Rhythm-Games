using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 対象：スクリプトをアタッチしたもの
/// カーソルをかざしたときに「音を再生する」
/// </summary>
public class SoundChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string _seName = default;
    private CriSoundManager _criSoundManager = default;
    private int _seIndex = default;

    private void Start()
    {
        _criSoundManager = CriSoundManager.Instance;
        if (_criSoundManager == null)
        {
            Debug.LogWarning($"{_criSoundManager}がありません。");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (string.IsNullOrEmpty(_seName))
            Debug.LogWarning($"objName: {gameObject.name} // SEの名前が空欄です。");
        else
            _seIndex = _criSoundManager.PlaySE(_seName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (string.IsNullOrEmpty(_seName))
            Debug.LogWarning("SEの名前が空欄です。");
        else
            _criSoundManager.StopSE(_seIndex);
    }
}