using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ポーズ画面からのシーン遷移時に使用
/// ポーズ後のTimeScaleを戻す
/// </summary>
public class RestoreTimeScale : MonoBehaviour
{
    [SerializeField] private Button _button = default;

    public void Start()
    {
        _button.onClick.AddListener(Restore);
    }

    private void Restore()
    {
        Time.timeScale = 1f;
    }
}