using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClickSceneChange);
    }

    public void OnClickSceneChange()
    {
        if (SceneLoad.I != null)
            SceneLoad.I.OnChangeScene("SelectMusicScene","TitleScene");
        else
            Debug.LogError("SceneLoadインスタンスを取得できていません");
    }
}
