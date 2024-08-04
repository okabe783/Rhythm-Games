using UnityEngine;

/// <summary> longnoteのlineを消す </summary>
public class DeleteLongNoteLine : MonoBehaviour
{
    [SerializeField, Header("JudgeZone")] private GameObject _judgeZone;

    private bool _isPressed = false;
    
    private Mesh _mesh;
    
    /// <summary> 元のmeshの頂点 </summary>
    private Vector3[] _vertices;

    private void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
        _vertices = _mesh.vertices;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            _isPressed = true;
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            _isPressed = false;
        }
        
        if (_isPressed)
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