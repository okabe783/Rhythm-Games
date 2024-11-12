using UnityEngine;
using UnityEngine.UI;

public class BackGroundMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Material _material;
    private const float Length = 1f;
    private const string PropName = "_MainTex";
    private static readonly int MainTex = Shader.PropertyToID(PropName);
    
    private void Start()
    {
        if (TryGetComponent(out Image image))
        {
            _material = image.material;
        }
    }

    private void Update()
    {
        if (_material)
        {
            var x = Mathf.Repeat(Time.time * _speed, Length);
            var offset = new Vector2(x, 0);
            _material.SetTextureOffset(MainTex, offset);
        }
    }
    
    private void OnDestroy()
    {
        if (_material)
        {
            _material.SetTextureOffset(MainTex, Vector2.zero);
        }
    }
}
