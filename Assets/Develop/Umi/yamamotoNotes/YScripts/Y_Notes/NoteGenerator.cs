using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary> ノーツの生成 </summary>
public class NoteGenerator : MonoBehaviour
{
    [SerializeField, Header("ノーツの種類")] private List<GameObject> _notesObj = new();
    [SerializeField, Header("このSceneの名前")] private string _sceneName;

    private Scene _scene = default;

    private void Start()
    {
        _scene = SceneManager.GetSceneByName(_sceneName);
    }

    public GameObject NoteGenerate(int noteType, Vector2 pos)
    {
        var note = Instantiate(_notesObj[noteType], pos, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(note, _scene);
        return note;
    }
    
    /// <summary> ロングノーツのラインの生成 </summary>
    /// <param name="startUp"> 左上 </param>
    /// <param name="startDown"> 左下 </param>
    /// <param name="finishUp"> 右上 </param>
    /// <param name="finishDown"> 右下 </param>
    public void NoteLineGenerate(Vector3 startUp, Vector3 startDown, Vector3 finishUp, Vector3 finishDown)
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
        SceneManager.MoveGameObjectToScene(lineObj, _scene);
    }
}
