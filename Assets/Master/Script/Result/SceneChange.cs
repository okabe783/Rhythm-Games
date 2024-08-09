using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public void OnClick()
    {
        SceneLoad.Instance.OnChangeScene("SelectMusicScene", "Result");
    }
}