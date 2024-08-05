using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YInput : MonoBehaviour
{
    [SerializeField] private NotesJudge notesJudge;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            notesJudge.TapNoteJudge(0);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            notesJudge.TapNoteJudge(1);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            notesJudge.LongNoteStartJudge(0);
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            notesJudge.LongNoteFinishJudge();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            notesJudge.LongNoteStartJudge(1);
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            notesJudge.LongNoteFinishJudge();
        }
    }
}
