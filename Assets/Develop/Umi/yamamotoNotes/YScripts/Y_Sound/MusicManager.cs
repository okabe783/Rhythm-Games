using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private string _music = default;
    private NotesManager _notesManager = default;
    
    private void Start()
    {
        _notesManager = FindObjectOfType<NotesManager>();
        var delay = _notesManager.Delay;
        Invoke("PlayMusic", delay);
    }

    private void PlayMusic()
    {
        CriSoundManager.Instance.PlayBGM(_music, 3f);
    }
}
