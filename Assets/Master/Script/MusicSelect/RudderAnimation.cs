using UnityEngine;

// 舵のアニメーションを制御
public class RudderAnimation : MonoBehaviour
{
    // 回転中かどうかを示すフラグ
    private bool _isRotating = false;

    // 目標回転角度
    private float _targetRotation;

    // 回転速度（1秒あたりに回転する角度）
    [SerializeField,Header("1秒あたりの回転するスピード")]private float _rotationSpeed = 180f;

    // 右回転か左回転かを示すフラグ
    private bool _rotateRight = true;

    private void Update()
    {
        if (_isRotating)
        {
            RotateObject();
        }
    }

    public void OnRotateRight()
    {
        //　回転中であるとき
        if (_isRotating)
        {
            //　右向きに回転していないのであれば
            if (!_rotateRight)
            {
                //　右向きにRotateを加える
                _targetRotation += 180f;
            }
            else
            {
                _targetRotation += 90f;
                _isRotating = true;
            }
        }
        else
        {
            // 初期回転
            _targetRotation = transform.eulerAngles.z + 90f;
            _isRotating = true;
        }

        _rotateRight = true;
    }

    // 左回転ボタンが押されたときの処理
    public void OnRotateLeft()
    {
        // 回転中であるとき
        if (_isRotating)
        {
            // 右向きに回転しているのであれば
            if (_rotateRight)
            {
                // 逆方向に回転させる
                _targetRotation -= 180f;
                // 左回転フラグに変更
                _rotateRight = false;
            }
            else
            {
                _targetRotation -= 90f;
                _rotateRight = false;
                _isRotating = true;
            }
        }
        else
        {
            // 初期回転
            _targetRotation = transform.eulerAngles.z - 90f;
            _rotateRight = false;
            _isRotating = true;
        }
    }

    private void RotateObject()
    {
        // 現在の回転角度と目標角度を比較して回転させる
        float step = _rotationSpeed * Time.deltaTime;
        float currentRotation = transform.eulerAngles.z;

        // 目標回転角度まで回転
        if (Mathf.Abs(Mathf.DeltaAngle(currentRotation, _targetRotation)) < step)
        {
            transform.eulerAngles = new Vector3(0f, 0f, _targetRotation);
            //　回転完了
            _isRotating = false;
        }
        else
        {
            // 回転方向を決定
            float rotationDirection = Mathf.Sign(_targetRotation - currentRotation);
            // Z軸回りに回転
            transform.Rotate(Vector3.forward, rotationDirection * step); 
        }
    }
}