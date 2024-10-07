using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public void OnClick()
    {
        CriSoundManager.Instance.StopBGM();
        SceneLoad.I.OnChangeScene("SelectMusicScene", "Result");
    }
}