using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ファイルから読み込んだ曲についてのデータ </summary>
[Serializable]
public class Data
{
    public int BPM;
    public int offset;
    public Note[] notes;
}

/// <summary> ファイルから読み込んだnoteのデータ </summary>
[Serializable] 
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
    public Note[] notes;
}

/// <summary> noteのデータを格納している </summary>
[Serializable]
public class NoteData
{
    public int noteType;
    public float noteTime;
    public GameObject noteObj;
}

/// <summary> ノーツの管理を行うクラス </summary>
public class NotesManager : MonoBehaviour
{
    [SerializeField] private float _noteSpeed;
    public float NoteSpeed => _noteSpeed;
    
    [SerializeField] private string _songName;

    [SerializeField] private float _delay;
    public float Delay => _delay;
    
    [SerializeField, Header("ノーツの生成が始まる場所")]
    private float _noteStartLine = -4.6f;

    /// <summary> 総ノーツ数 </summary>
    private int _notesNum;
    
    /// <summary> longnoteの終わりのtype </summary>
    private readonly int _noteFinishType = 3;

    private NoteGenerator _noteGenerator = default;

    private List<List<NoteData>> _notesLists = default;
    private void Start()
    {
        _notesLists = new List<List<NoteData>>()
        {
            new List<NoteData>(), //uplist
            new List<NoteData>() //downlist
        };
        _noteGenerator = gameObject.GetComponent<NoteGenerator>();
        _notesNum = 0;
        Invoke("Load", _delay);
    }

    private void Load()
    {
        string inputString = Resources.Load<TextAsset>(_songName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        _notesNum = inputJson.notes.Length;

        for (int i = 0; i < _notesNum; i++)
        {
            // ノーツの流れてくる時間
            float time =
                (60 / (float)inputJson.BPM * inputJson.notes[i].num / inputJson.notes[i].LPB)
                + inputJson.offset * 0.01f;

            // ノーツの生成
            float x = time * _noteSpeed + _noteStartLine; // ノーツのx座標
            float y = inputJson.notes[i].block * -3f + 1f; // ノーツのy座標
            var note = _noteGenerator.NoteGenerate(inputJson.notes[i].type, new Vector2(x, y));
            var startUp = new Vector3(x, y + note.transform.localScale.y / 2);
            var startDown = new Vector3(x, y - note.transform.localScale.y / 2);

            var lane = inputJson.notes[i].block;
            _notesLists[lane].Add(new NoteData()
            {
                noteType = inputJson.notes[i].type, noteTime = time, noteObj = note
            });
            if (inputJson.notes[i].type == 2)
            {
                float finishTime = 
                    (60 / (float)inputJson.BPM * inputJson.notes[i].notes[0].num / inputJson.notes[i].notes[0].LPB) 
                        + inputJson.offset * 0.01f;
                _notesLists[lane].Add(new NoteData()
                {
                    noteType = _noteFinishType, noteTime = finishTime, 
                    noteObj = _noteGenerator.NoteGenerate(inputJson.notes[i].type, 
                        new Vector2(finishTime * _noteSpeed + _noteStartLine, y)) 
                });
                var finishUp = new Vector3(finishTime * _noteSpeed + _noteStartLine, 
                    y + _notesLists[lane][^1].noteObj.transform.localScale.y / 2); 
                var finishDown = new Vector3(finishTime * _noteSpeed + _noteStartLine,
                    y - _notesLists[lane][^1].noteObj.transform.localScale.y / 2);
                _noteGenerator.NoteLineGenerate(startUp, startDown, finishUp, finishDown);
            }
        }
    }

    /// <summary> noteのデータを削除する </summary>
    /// <param name="lane"> どっちのレーンか（0 = 上, 1 = 下）</param>
    /// <param name="visible"> true = judgezoneで消す, false = 画面外に行ったら消す</param>
    public void DeleteNoteData(int lane, bool visible)
    {
        _notesLists[lane][0].noteObj.SetActive(visible);
        _notesLists[lane].RemoveAt(0);
    }

    /// <summary> TapNoteの流れてくる時間を取得する関数 </summary>
    /// もしなかったら絶対にない値(-1)を返す
    /// <param name="lane"> どっちのレーンか（0 = 上, 1 = 下）</param>
    /// <returns> noteの時間 </returns>
    public float GetTapNotesData(int lane)
    {
        if (_notesLists[lane].Count == 0) { return -1; } // 指定されたレーンにノーツがすでにないとき
        for (int i = 0; i < _notesLists[lane].Count; i++)
        {
            if (_notesLists[lane][i].noteType == 1)
            {
                return _notesLists[lane][i].noteTime;
            }
        }
        return -1;
    }

    /// <summary> LongNoteの流れてくる時間を取得する関数 </summary>
    /// もしなかったら絶対にない値(-1, -1)を返す
    /// <param name="lane"> どっちのレーンか（0 = 上, 1 = 下）</param>
    /// <returns> noteの時間, noteの長さ </returns>
    public (float, float) GetLongNotesData(int lane)
    {
        if (_notesLists[lane].Count == 0) { return (-1, -1); } // 指定されたレーンにノーツがすでにないとき
        for (int i = 0; i < _notesLists[lane].Count; i++)
        {
            if (_notesLists[lane][i].noteType == 2) // 押されたとき次がlongnoteの始まりだったら
            { 
                float noteDuration = _notesLists[lane][i + 1].noteTime - _notesLists[lane][i].noteTime;
                return (_notesLists[lane][i].noteTime, noteDuration);
            }
            else if (_notesLists[lane][i].noteType == _noteFinishType) // 押されたとき次がlongnoteの終わりだったら
            {
                return (_notesLists[lane][i].noteTime, -1);
            }
        }
        return (-1, -1);
    }
}