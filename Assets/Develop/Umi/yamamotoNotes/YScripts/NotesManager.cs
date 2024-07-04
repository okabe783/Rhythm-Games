using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


/// <summary> ファイルから読み込んだ曲についてのデータ </summary>
[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
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
    public float noteDuration;
    public GameObject noteObj;
}

public class NotesManager : MonoBehaviour
{
    /// <summary> 総ノーツ数 </summary>
    private int _notesNum;

    /// <summary> 曲名 </summary>
    private string _songName;

    /// <summary> 上のレーンに流れてくるノーツのリスト </summary>>
    private List<NoteData> UpNoteList;
    
    /// <summary> 下のレーンに流れてくるノーツのリスト </summary>>
    private List<NoteData> DownNoteList;

    [SerializeField, Tooltip("ノーツのスピード")] 
    private float noteSpeed;
    
    [SerializeField, Tooltip("ノーツの種類")]
    private List<GameObject> notesObj = new List<GameObject>();

    private void OnEnable()
    {
        UpNoteList = new List<NoteData>();
        DownNoteList = new List<NoteData>();
        _notesNum = 0;
        _songName = ""; // 選択された曲名をここに入れる
        Load(_songName);
    }

    private void Load(string songName)
    {
        string inputString = Resources.Load<TextAsset>(songName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        _notesNum = inputJson.notes.Length;

        for (int i = 0; i < _notesNum; i++)
        {
            // 一小節の長さ
            float barLength = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            
            // ノーツ間の長さ
            float beatSec = barLength * (float)inputJson.notes[i].LPB;
            
            // ノーツの流れてくる時間
            float time = (beatSec * inputJson.notes[i].num / inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            
            float noteDuration = 0;
            if (inputJson.notes[i].type == 2) // LongNoteだった時のnoteの長さ
            {
                float finishTime = (beatSec * inputJson.notes[i].notes[0].num / inputJson.notes[i].notes[0].LPB) + inputJson.offset * 0.01f;
                noteDuration = finishTime - time;
            }
            
            // ノーツの生成
            float x = time * noteSpeed * 0.8f + 3.5f;
            var note = Instantiate(notesObj[inputJson.notes[i].type - 1], new Vector2(x, -2f * inputJson.notes[i].block + 1f), Quaternion.identity);

            if (inputJson.notes[i].block == 0) // 上と下それぞれのリストに格納する
            {
                UpNoteList.Add(new NoteData()
                {
                    noteType = inputJson.notes[i].type, noteTime = time, noteDuration = noteDuration, noteObj = note
                });
            }
            else if (inputJson.notes[i].block == 1)
            {
                DownNoteList.Add(new NoteData()
                {
                    noteType = inputJson.notes[i].type, noteTime = time, noteDuration = noteDuration, noteObj = note
                });
            }
        }
    }

    /// <summary> noteのデータを削除する </summary>
    /// <param name="lane"> どっちのレーンか（0 = 上, 1 = 下）</param>
    public void DeleteNoteData(int lane)
    {
        if (lane == 0)
        {
            UpNoteList.RemoveAt(0);
            UpNoteList.RemoveAt(0);
        }
        else if (lane == 1)
        {
            DownNoteList.RemoveAt(0);
            DownNoteList.RemoveAt(0);
        }
    }
    
    /// <summary> TapNoteの流れてくる時間を取得する関数 </summary>
    /// <param name="lane"> どっちのレーンか（0 = 上, 1 = 下）</param>
    /// <returns> noteの時間 </returns>
    public float GetTapNotesData(int lane)
    {
        if (lane == 0)
        {
            for (int i = 0; i < UpNoteList.Count; i++)
            {
                if (UpNoteList[i].noteType == 1)
                {
                    return UpNoteList[i].noteTime;
                }
            }
        }
        else if (lane == 1)
        {
            for (int i = 0; i < DownNoteList.Count; i++)
            {
                if (DownNoteList[i].noteType == 1)
                {
                    return DownNoteList[i].noteTime;
                }
            }
        }

        return -1;
    }
    
    /// <summary> TapNoteの流れてくる時間を取得する関数 </summary>
    /// <param name="lane"> どっちのレーンか（0 = 上, 1 = 下）</param>
    /// <returns> noteの時間, noteの長さ </returns>
    public (float, float) GetLongNotesData(int lane) 
    {
        if (lane == 0)
        {
            for (int i = 0; i < UpNoteList.Count; i++)
            {
                if (UpNoteList[i].noteType == 2)
                {
                    return (UpNoteList[i].noteTime, UpNoteList[i].noteDuration);
                }
            }
        }
        else if (lane == 1)
        {
            for (int i = 0; i < DownNoteList.Count; i++)
            {
                if (DownNoteList[i].noteType == 2)
                {
                    return (DownNoteList[i].noteTime, DownNoteList[i].noteDuration);
                }
            }
        }

        return (-1, -1);
    }
}
