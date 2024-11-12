using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームを終了する
/// </summary>
public class ExitGame : MonoBehaviour
{
    [SerializeField] private Button _button = default;

    private void Start()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}