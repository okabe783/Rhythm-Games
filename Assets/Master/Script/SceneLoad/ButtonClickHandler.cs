using UnityEngine;

public class ButtonClickHandler : MonoBehaviour
{
    public void OnClickSceneChange()
    {
        if (SceneLoad.Instance != null)
            SceneLoad.Instance.OnClickSelectMusic();
        else
            Debug.LogError("SceneLoadインスタンスを取得できていません");
    }
}
