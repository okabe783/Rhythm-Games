using UnityEngine;

/// <summary>
/// サウンド呼び出しテスト用
/// </summary>
public class CriSoundTest : MonoBehaviour
{
    [SerializeField, Header("CriSoundManager")]
    private CriSoundManager _criManager;
    
    void Update()
    {
        // 左クリック
        if (Input.GetButtonDown("Fire1"))
        {
            _criManager.PlayBGM("MUSIC_ShiningStarShort");
            _criManager.PlaySE("SE_syan", 0.5f);
            //_criManager.PlaySE("SE_kon");
            //_criManager.BGMVolume = 0.3f;
        }

        // ホイールクリック
        if (Input.GetButtonDown("Fire3"))
        {
            // 一時停止
            _criManager.ResumeBGM();
        }
        
        // 右クリック
        if (Input.GetButtonDown("Fire2"))
        {
            // 再生
            _criManager.PauseBGM();
        }
    }
}
