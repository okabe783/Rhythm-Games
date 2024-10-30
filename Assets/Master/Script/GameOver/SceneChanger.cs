using UnityEngine;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string _sceneName = default;
    [SerializeField] private string _unloadSceneName = default;
    [SerializeField] private Button _button = default;

    private void Start()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        SceneLoad.I.OnChangeScene(_sceneName, _unloadSceneName);
    }
}