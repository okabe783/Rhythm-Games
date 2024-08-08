using UnityEngine;

/// <summary> InGameのPause </summary>
public class InGamePause : Pause
{
    [SerializeField] private Canvas _canvas = null;
    public bool _isPause { get; private set; }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isPause)
        {
            Pause();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && _isPause)
        {
            Resume();
        }
    }

    private void Pause()
    {
        _isPause = true;
        DrawPauseMenu(_canvas);
        Time.timeScale = 0f;
        Debug.Log("Pause");
    }

    private void Resume()
    {
        _isPause = false;
        DeletePauseMenu(_canvas);
        Time.timeScale = 1f;
        Debug.Log("Resume");
    }
}
