using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField] private string _bossName;
    private NotesManager _notesManager;
    private GameObject _boss;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _notesManager = FindObjectOfType<NotesManager>();
        if (_bossName != "")
        {
            _boss = GameObject.Find(_bossName);
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = new Color(255, 255, 255, 0);
        }
    }

    private void Update()
    {
        transform.position -= transform.right * (_notesManager.NoteSpeed * Time.deltaTime);
        if (_spriteRenderer)
        {
            if (transform.position.x < _boss.transform.position.x) _spriteRenderer.color = new Color(255, 255, 255, 255);
        }

        if (transform.position.x < -10f)
        {
            gameObject.SetActive(false);
        }
    }
}
