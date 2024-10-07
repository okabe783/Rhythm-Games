using UnityEngine;

public class BackButton : MonoBehaviour
{
    [SerializeField] private BackState _backState;

    private void OnClickBackButton()
    {
        switch (_backState)
        {
            case BackState.BackCharacterScene:
                SceneLoad.I.OnChangeScene("", "");
                break;
            case BackState.BackTitleScene:
                SceneLoad.I.OnChangeScene("", "");
                break;
        }
    }
}