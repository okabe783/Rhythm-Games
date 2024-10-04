using UnityEngine;

/// <summary> InGameのPause </summary>
public class InGamePause : Pause
{
    [SerializeField] private Canvas _canvas = null;
    [SerializeField] private MusicManager _musicManager;
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
        _musicManager.Stop();
        DrawPauseMenu(_canvas);
        Time.timeScale = 0f;
    }

    private void Resume()
    {
        _isPause = false;
        DeletePauseMenu(_canvas);
        Time.timeScale = 1f;
        _musicManager.Resume();
    }
}
