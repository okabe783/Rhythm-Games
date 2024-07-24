using System;
using UnityEngine;

/// <summary>
/// サウンド呼び出しテスト用
/// </summary>
public class CriSoundTest : MonoBehaviour
{
    //[SerializeField, Header("CriSoundManager")]
    //private CriSoundManager _criManager;
    private CriSoundManager _criManager;

    private void Start()
    {
        _criManager = CriSoundManager.Instance;
        Debug.Log("左クリックで再生\n右クリックでポーズ\nホイールクリックでポーズ解除\nＫで停止\nPで楽曲再生");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P");
            _criManager.PlayBGM("MUSIC_ShiningStarShort"); //MUSIC_ShiningStarShort
        }

        // 左クリック
        if (Input.GetButtonDown("Fire1"))
        {
            _criManager.PlaySE("SE_syan", 0.5f);
            //_criManager.PlaySE("SE_kon");
            //_criManager.BGMVolume = 0.3f;
            Debug.Log("左クリック：再生");
        }

        // ホイールクリック
        if (Input.GetButtonDown("Fire3"))
        {
            // 一時停止解除
            _criManager.ResumeBGM();
            Debug.Log("ホイールクリック：一時停止解除");
        }
        
        // スペース
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            // 音量変更
            Debug.Log("音量変更");
            _criManager.BGMVolume = 0.1f;
        }*/
        
        if (Input.GetButtonDown("Fire2"))
        {
            // 一時停止
            _criManager.PauseBGM();
            Debug.Log("右クリック：一時停止");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //_soundManager.ResumeAll();
            _criManager.StopBGM();
            Debug.Log("K：停止");
        }
    }
}
