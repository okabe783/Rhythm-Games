using UnityEngine;

/// <summary>クリックされた時にUIが移動するポイントを管理する</summary>
public class MoveCellToPoints : MonoBehaviour
{
    // 点の数
    private int _point = 8;
    // 円の半径
    private float _radius = 20f;
    // 円の中心座標
    private Vector2 _center = new(0, 0); 

    public Vector2[] GetCircularPoints()
    {
        // 点の配列
        Vector2[] points = new Vector2[_point];
        
        // 角度の分割
        float angleStep = 360f / _point; 

        for (int i = 0; i < _point; i++)
        {
            // ラジアンに変換
            float angle = i * angleStep * Mathf.Deg2Rad; 
            float x = _center.x + _radius * Mathf.Cos(angle);
            float y = _center.y + _radius * Mathf.Sin(angle);
            points[i] = new Vector2(x, y);
        }

        return points;
    }

    // Editor上に可視化する
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector2[] points = GetCircularPoints();
        Gizmos.color = Color.red;

        foreach (Vector2 point in points)
        {
            Gizmos.DrawSphere(new Vector3(point.x,point.y,0),1f);
        }
    }
    #endif
}
