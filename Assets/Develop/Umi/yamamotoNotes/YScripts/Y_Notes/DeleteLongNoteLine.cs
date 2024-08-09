using Unity.VisualScripting;
using UnityEngine;

/// <summary> longnoteのlineを消す </summary>
public class DeleteLongNoteLine : MonoBehaviour
{
    private GameObject _judgeZone = default;

    private Mesh _mesh = default;

    private PlayerInput _playerInput = default;
    
    /// <summary> 元のmeshの頂点 </summary>
    private Vector3[] _vertices;

    private void Start()
    {
        _judgeZone = GameObject.Find("JudgeZone");
        _playerInput = FindObjectOfType<PlayerInput>();
        _mesh = GetComponent<MeshFilter>().mesh;
        _vertices = _mesh.vertices;
    }

    private void Update()
    {
        if (_playerInput.IsLongPress)
        {
            Vector3[] vertices = new Vector3[_vertices.Length];
            for (var i = 0; i < vertices.Length; i++) // meshの頂点の位置を調整していく
            {
                vertices[i] = _vertices[i];
                if (transform.TransformPoint(vertices[i]).x < _judgeZone.transform.position.x)
                {
                    vertices[i] = new Vector3(_judgeZone.transform.position.x - transform.position.x,
                        vertices[i].y);
                }

                _mesh.vertices = vertices;
                _mesh.RecalculateBounds();
            }
        }

        if (transform.position.x < -10f)
        {
            gameObject.SetActive(false);
        }
    }
}