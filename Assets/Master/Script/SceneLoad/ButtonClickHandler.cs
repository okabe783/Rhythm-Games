using UnityEngine;

public class ButtonClickHandler : MonoBehaviour
{
    public void OnClickSceneChange()
    {
        if (SceneLoad.I != null)
            SceneLoad.I.OnChangeScene("SelectMusicScene","TitleScene");
        else
            Debug.LogError("SceneLoadインスタンスを取得できていません");
    }
}
