using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Pauseの基底クラス </summary>
public class Pause : MonoBehaviour
{
    protected void DrawPauseMenu(Canvas can)
    {
        CriSoundManager.Instance.PlaySE("SE_Pause", 0.5f);
        can.gameObject.SetActive(true);
    }

    protected void DeletePauseMenu(Canvas can)
    {
        can.gameObject.SetActive(false);
    }
}
