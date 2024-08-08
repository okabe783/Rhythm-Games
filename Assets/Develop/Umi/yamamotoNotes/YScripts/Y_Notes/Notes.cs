using UnityEngine;

public class Notes : MonoBehaviour
{
    private NotesManager _notesManager;

    private void Start()
    {
        _notesManager = FindObjectOfType<NotesManager>();
    }

    private void Update()
    {
        transform.position -= transform.right * (_notesManager.NoteSpeed * Time.deltaTime);
    }
}
