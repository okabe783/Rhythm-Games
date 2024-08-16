using System;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject noteObj;
}

/// <summary> ノーツの生成や管理を行うクラス </summary>
public class NotesManager : MonoBehaviour
{
    [SerializeField, Header("ノーツのスピード")]
    private float _noteSpeed;
    
    public float NoteSpeed  => _noteSpeed; 
    
    [SerializeField, Header("ノーツの種類")]
    private List<GameObject> _notesObj = new List<GameObject>();
    
    [SerializeField, Header("ノーツの生成が始まる場所")]
    private float _noteStartLine = -4.6f;
    
    [SerializeField, Header("曲名")]
    private string _songName;
    
    /// <summary> 総ノーツ数 </summary>
    private int _notesNum;

    /// <summary> 上のレーンに流れてくるノーツのリスト </summary>>
    private List<NoteData> _upNoteList;
    
    /// <summary> 下のレーンに流れてくるノーツのリスト </summary>>
    private List<NoteData> _downNoteList;
    
    /// <summary> longnoteの終わりのtype </summary>
    readonly int _noteFinishType = 3;
    
    private void OnEnable()
    {
        _upNoteList = new List<NoteData>();
        _downNoteList = new List<NoteData>();
        _notesNum = 0;
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
            float time = 
                (beatSec * inputJson.notes[i].num / inputJson.notes[i].LPB) 
                         + inputJson.offset * 0.01f;
            
            // ノーツの生成
            float x = time * _noteSpeed + _noteStartLine; // ノーツのx座標
            float y = inputJson.notes[i].block * -4f + 2f; // ノーツのy座標
            var note = 
                Instantiate(_notesObj[inputJson.notes[i].type - 1],
                    new Vector2(x, y), Quaternion.identity);
            var startUp = new Vector3(x, y + _notesObj[1].transform.localScale.y / 2);
            var startDown = new Vector3(x, y - _notesObj[1].transform.localScale.y / 2);

            if (inputJson.notes[i].block == 0) // 上と下それぞれのリストに入れる
            {
                _upNoteList.Add(new NoteData()
                {
                    noteType = inputJson.notes[i].type, noteTime = time, noteObj = note
                });
                if (inputJson.notes[i].type == 2) // ロングノーツは終わりの時間もリストに入れる
                {
                    float finishTime = 
                        (beatSec * inputJson.notes[i].notes[0].num / inputJson.notes[i].notes[0].LPB) 
                        + inputJson.offset * 0.01f;
                    _upNoteList.Add(new NoteData()
                    {
                        noteType = _noteFinishType, noteTime = finishTime,
                        noteObj = 
                            Instantiate(_notesObj[inputJson.notes[i].type - 1],
                            new Vector2(finishTime * _noteSpeed + _noteStartLine, y),
                            Quaternion.identity)
                    });
                    var finishUp = new Vector3(finishTime * _noteSpeed + _noteStartLine,
                        y + _notesObj[1].transform.localScale.y / 2);
                    var finishDown = new Vector3(finishTime * _noteSpeed + _noteStartLine,
                        y - _notesObj[1].transform.localScale.y / 2);
                    NoteLineGenerate(startUp, startDown, finishUp, finishDown);
                }
            }
            else if (inputJson.notes[i].block == 1)
            {
                _downNoteList.Add(new NoteData()
                {
                    noteType = inputJson.notes[i].type, noteTime = time, noteObj = note
                });
                if (inputJson.notes[i].type == 2) // ロングノーツは終わりの時間もリストに入れる
                {
                    float finishTime = 
                        (beatSec * inputJson.notes[i].notes[0].num / inputJson.notes[i].notes[0].LPB) 
                        + inputJson.offset * 0.01f;
                    _downNoteList.Add(new NoteData()
                    {
                        noteType = _noteFinishType, noteTime = finishTime,
                        noteObj = 
                            Instantiate(_notesObj[inputJson.notes[i].type - 1],
                            new Vector2(finishTime * _noteSpeed + _noteStartLine, y),
                            Quaternion.identity)
                    });
                    var finishUp = new Vector3(finishTime * _noteSpeed + _noteStartLine,
                        y + _notesObj[1].transform.localScale.y / 2);
                    var finishDown = new Vector3(finishTime * _noteSpeed + _noteStartLine,
                        y - _notesObj[1].transform.localScale.y / 2);
                    NoteLineGenerate(startUp, startDown, finishUp, finishDown);
                }
            }
        }
    }

    /// <summary> noteのデータを削除する </summary>
    /// <param name="lane"> どっちのレーンか（0 = 上, 1 = 下）</param>
    /// <param name="visible"> true = judgezoneで消す, false = 画面外に行ったら消す</param>
    public void DeleteNoteData(int lane, bool visible)
    {
        if (lane == 0)
        {
            _upNoteList[0].noteObj.SetActive(visible);
            _upNoteList.RemoveAt(0);
        }
        else if (lane == 1)
        {
            _downNoteList[0].noteObj.SetActive(visible);
            _downNoteList.RemoveAt(0);
        }
    }
    
    /// <summary> TapNoteの流れてくる時間を取得する関数 </summary>
    /// もしなかったら絶対にない値(-1)を返す
    /// <param name="lane"> どっちのレーンか（0 = 上, 1 = 下）</param>
    /// <returns> noteの時間 </returns>
    public float GetTapNotesData(int lane)
    {
        if (lane == 0)
        {
            if (_upNoteList.Count == 0) { return -1; }
            for (int i = 0; i < _upNoteList.Count; i++)
            {
                if (_upNoteList[i].noteType == 1)
                {
                    return _upNoteList[i].noteTime;
                }
            }
        }
        else if (lane == 1)
        {
            if (_downNoteList.Count == 0) { return -1; }
            for (int i = 0; i < _downNoteList.Count; i++)
            {
                if (_downNoteList[i].noteType == 1)
                {
                    return _downNoteList[i].noteTime;
                }
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
        if (lane == 0)
        {
            if (_upNoteList.Count == 0) { return (-1, -1); }
            for (int i = 0; i < _upNoteList.Count; i++)
            {
                if (_upNoteList[i].noteType == 2) // 押されたとき次がlongnoteの始まりだったら
                {
                    float noteDuration = _upNoteList[i + 1].noteTime - _upNoteList[i].noteTime;
                    return (_upNoteList[i].noteTime, noteDuration);
                }

                if (_upNoteList[i].noteType == _noteFinishType) // 押されたとき次がlongnoteの終わりだったら
                {
                    return (_upNoteList[i].noteTime, -1);
                }
            }
        }
        else if (lane == 1)
        {
            if (_downNoteList.Count == 0) { return (-1, -1); }
            for (int i = 0; i < _downNoteList.Count; i++)
            {
                if (_downNoteList[i].noteType == 2) // 押されたとき次に来るlongnoteがlongnoteの始まりだったら
                {
                    float noteDuration = _downNoteList[i + 1].noteTime - _downNoteList[i].noteTime;
                    return (_downNoteList[i].noteTime, noteDuration);
                }

                if (_downNoteList[i].noteType == _noteFinishType) // 押されたとき次がlongnoteの終わりだったら
                {
                    return (_downNoteList[i].noteTime, -1);
                }
            }
        }

        return (-1, -1);
    }

    /// <summary> ロングノーツのラインの生成 </summary>
    /// <param name="startUp"> ロングノーツのはじまりの上 </param>
    /// <param name="startDown"> ロングノーツのはじまりの下 </param>
    /// <param name="finishUp"> ロングノーツのおわりの上 </param>
    /// <param name="finishDown"> ロングノーツのおわりの下 </param>
    private void NoteLineGenerate(Vector3 startUp, Vector3 startDown, Vector3 finishUp, Vector3 finishDown)
    {
        int[] triangles = new int[6] { 0, 2, 1, 3, 1, 2 };
        var vertices = new Vector3[4];
        vertices[0] = new Vector3(-(finishDown.x - startDown.x), -(finishUp.y - startDown.y));
        vertices[1] = new Vector3(0, -(finishUp.y - finishDown.y));
        vertices[2] = new Vector3(-(finishUp.x - startUp.x), 0);
        vertices[3] = Vector3.zero;
        GameObject lineObj = new GameObject();
        lineObj.AddComponent<MeshFilter>();
        lineObj.AddComponent<MeshRenderer>();
        lineObj.AddComponent<Notes>();
        lineObj.AddComponent<DeleteLongNoteLine>();
        Mesh mesh = new Mesh();
        lineObj.GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        lineObj.transform.position = finishUp;
    }
}
