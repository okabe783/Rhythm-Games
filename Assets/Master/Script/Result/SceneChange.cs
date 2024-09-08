using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public void OnClick()
    {
        CriSoundManager.Instance.StopBGM();
        SceneLoad.Instance.OnChangeScene("SelectMusicScene", "Result");
    }
}