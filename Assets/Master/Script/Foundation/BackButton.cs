using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] private BackState _backState;
    [SerializeField,Header("UnLoadしたいシーン")] private string _sceneName;
    [SerializeField,Header("戻るボタン")] private Button _backButton;

    private void Start()
    {
        _backButton.onClick.AddListener(OnClickBackButton);
    }

    private void OnClickBackButton()
    {
        switch (_backState)
        {
            case BackState.BackCharacterScene:
                SceneLoad.I.OnChangeScene("", "");
                break;
            case BackState.BackTitleScene:
                SceneLoad.I.OnChangeScene("TitleScene", _sceneName);
                break;
        }
    }
}