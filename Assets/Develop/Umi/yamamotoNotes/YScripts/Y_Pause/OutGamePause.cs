using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> OutGameのPause </summary>
public class OutGamePause : Pause
{
    [SerializeField] private Canvas _canvas = null;

    public void Pause()
    {
        DrawPauseMenu(_canvas);
    }

    public void Resume()
    {
        DeletePauseMenu(_canvas);
    }
}
