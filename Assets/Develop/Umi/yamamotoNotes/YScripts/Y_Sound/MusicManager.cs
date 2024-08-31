using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private NotesManager _notesManager = default;
    
    private void Start()
    {
        _notesManager = FindObjectOfType<NotesManager>();
        var delay = _notesManager.Delay;
        Invoke("PlayMusic", delay);
    }

    private void PlayMusic()
    {
        CriSoundManager.Instance.PlayBGM("MUSIC_Retentir", 3f);
    }
}
