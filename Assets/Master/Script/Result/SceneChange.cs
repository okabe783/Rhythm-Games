using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void OnClick(string sceneName)
    {
        if (SceneLoad.Instance != null)
            SceneLoad.Instance.StartLongLoad(sceneName, SceneManager.GetActiveScene().name);
        else Debug.LogWarning("SceneLoadのインスタンスが見つかりませんでした。");
    }
}