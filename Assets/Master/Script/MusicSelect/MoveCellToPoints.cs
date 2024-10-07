using UnityEngine;

public class MoveCellToPoints : MonoBehaviour
{
    //点の数
    private int _point = 8;
    //円の半径
    private float _radius = 20f;
    //円の中心座標
    private Vector2 _center = new(0, 0); 

    public Vector2[] GetCircularPoints()
    {
        Vector2[] points = new Vector2[_point];
        float angleStep = 360f / _point; //角度の分割

        for (int i = 0; i < _point; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad; //ラジアンに変換
            float x = _center.x + _radius * Mathf.Cos(angle);
            float y = _center.y + _radius * Mathf.Sin(angle);
            points[i] = new Vector2(x, y);
        }

        return points;
    }

    private void OnDrawGizmos()
    {
        Vector2[] points = GetCircularPoints();
        Gizmos.color = Color.red;

        foreach (var point in points)
        {
            Gizmos.DrawSphere(new Vector3(point.x,point.y,0),1f);
        }
    }
}
