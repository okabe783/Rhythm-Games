using UnityEngine;

public class ButtonClickHandler : MonoBehaviour
{
    public void OnClickSceneChange()
    {
        if (SceneLoad.Instance != null)
            SceneLoad.Instance.OnChangeScene("SelectMusicScene","TitleScene");
        else
            Debug.LogError("SceneLoadインスタンスを取得できていません");
    }
}
