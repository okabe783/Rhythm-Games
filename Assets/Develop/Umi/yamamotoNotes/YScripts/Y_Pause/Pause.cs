using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Pauseの基底クラス </summary>
public class Pause : MonoBehaviour
{
    protected void DrawPauseMenu(Canvas can)
    {
        can.gameObject.SetActive(true);
    }

    protected void DeletePauseMenu(Canvas can)
    {
        can.gameObject.SetActive(false);
    }
}
