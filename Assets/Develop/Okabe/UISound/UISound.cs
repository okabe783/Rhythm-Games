using UnityEngine;

public class UISound : MonoBehaviour
{
    //舵の音を再生
    public void RudderSound()
    {
        CriSoundManager.Instance.PlaySE("SE_Kishimuoto", 0.5f);
        Debug.Log("aaa");
    }
}
