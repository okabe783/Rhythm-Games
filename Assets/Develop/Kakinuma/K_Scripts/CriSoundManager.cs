using System;
using System.Collections.Generic;
using CriWare;
using UnityEngine;

/// <summary>
/// シーン上に必要なもの
/// CriWareLibraryInitializerオブジェクト
/// CriWareErrorHandlerオブジェクト
/// CriAtomコンポーネントが付いたオブジェクト
/// </summary>
public class CriSoundManager : MonoBehaviour
{
    [SerializeField, Tooltip("ACFファイルの名前")]
    private string _streamingAssetsAcf;

    [SerializeField, Header("BGMのキューシートの名前")]
    private string _cueSheetBGM; // .acb

    [SerializeField, Header("BGM用awbのパス名")]
    private string _awbPathBGM; // .awb ストリーム再生用

    [SerializeField, Header("SEのキューシートの名前")]
    private string _cueSheetSE; // .acb

#region "CriAtomExPLayerとそれぞれのデータ"

    // BGM
    private CriAtomExPlayer _bgmPlayer;
    private CriAtomExPlayer _bgmLoopPlayer;
    private CriPlayerData _bgmData;
    
    // SE
    private CriAtomExPlayer _sePlayer;
    private List<CriPlayerData> _seData;
    
    private string _currentBGMCueName = "";
    private CriAtomExAcb _currentBGMAcb = null;

#endregion

#region "音量関係"
    
    private float _masterVolume = 1f;
    private float _bgmVolume = 1f;
    private float _seVolume = 1f;
    private const float Diff = 0.01f; //音量の変更があったかどうかの判定に使う ★定数の表記方法確認する

    /// <summary>マスターボリュームが変更された際に呼ばれるEvent</summary>
    public Action<float> MasterVolumeChanged;

    /// <summary>BGMボリュームが変更された際に呼ばれるEvent</summary>
    public Action<float> BGMVolumeChanged;
    
    /// <summary>SEボリュームが変更された際に呼ばれるEvent</summary>
    public Action<float> SEVolumeChanged;

#endregion

#region "音量変更"

    /// <summary>マスターボリューム</summary>
    /// <value>変更したい値</value>
    public float MasterVolume
    {
        get => _masterVolume;
        set
        {
            if (!(_masterVolume + Diff < value) && !(_masterVolume - Diff > value)) return;
            MasterVolumeChanged.Invoke(value);
            _masterVolume = value;
        }
    }

    /// <summary>BGMボリューム</summary>
    /// <value>変更したい値</value>
    public float BGMVolume
    {
        get => _bgmVolume;
        set
        {
            if (!(_bgmVolume + Diff < value) && !(_bgmVolume - Diff > value)) return;
            BGMVolumeChanged.Invoke(value);
            _bgmVolume = value;
        }
    }

    /// <summary>SEボリューム</summary>
    /// <value>変更したい値</value>
    public float SEVolume
    {
        get => _seVolume;
        set
        {
            if (!(_seVolume + Diff < value) && !(_seVolume - Diff > value)) return;
            SEVolumeChanged.Invoke(value);
            _seVolume = value;
        }
    }

#endregion

    /// <summary>PlayerとPlayback</summary>
    private struct CriPlayerData
    {
        public CriAtomExPlayback Playback { get; set; }
        public CriAtomEx.CueInfo CueInfo { get; set; }
        public bool IsLoop => CueInfo.length < 0; // いらないかも
    }
    
    private void Awake()
    {
        // acfを設定する
        string path = Application.streamingAssetsPath + $"/{_streamingAssetsAcf}.acf";
        CriAtomEx.RegisterAcf(null, path);
        // BGM acb追加 あればawbも
        CriAtom.AddCueSheet(_cueSheetBGM, $"{_cueSheetBGM}.acb",
            _awbPathBGM != ""? $"{_awbPathBGM}.awb" : null, null);
        // SE acb追加
        CriAtom.AddCueSheet(_cueSheetSE, $"{_cueSheetSE}.acb", null, null);
        
        _bgmPlayer = new CriAtomExPlayer();
        _bgmLoopPlayer = new CriAtomExPlayer();
        
        _sePlayer = new CriAtomExPlayer();
        _seData = new List<CriPlayerData>();
        
        // 音量を初期化
        InitializeVolume();

        //SceneManager.sceneUnloaded += Unload;   // リソース解放
    }

    /*private void OnDestroy()
    {
        SceneManager.sceneUnloaded += Unload;   // リソース解放
    }*/

    /// <summary>音量を初期化、変更する</summary>
    private void InitializeVolume()
    {
        // Masterが変更されたら全ての音量を変更する
        MasterVolumeChanged += volume =>
        {
            // BGM
            _bgmPlayer.SetVolume(volume * _bgmVolume);
            _bgmPlayer.Update(_bgmData.Playback);
            
            _bgmLoopPlayer.SetVolume(volume * _bgmVolume);
            _bgmLoopPlayer.Update(_bgmData.Playback);
            
            // SE
            foreach (var se in _seData)
            {
                _sePlayer.SetVolume(volume * _seVolume);
                _sePlayer.Update(se.Playback);
            }
        };
        
        // BGMのみ音量変更があった場合 現状楽曲もBGMも同じ音量
        BGMVolumeChanged += volume =>
        {
            _bgmPlayer.SetVolume(_masterVolume * volume);
            _bgmPlayer.Update(_bgmData.Playback);
            _bgmLoopPlayer.SetVolume(_masterVolume * volume);
            _bgmLoopPlayer.Update(_bgmData.Playback);
        };

        // SEのみ音量変更があった場合
        SEVolumeChanged += volume =>
        {
            foreach (var se in _seData)
            {
                _sePlayer.SetVolume(_masterVolume * volume);
                _sePlayer.Update(se.Playback);
            }
        };
    }
    

#region "全ての音に対する処理"

    /// <summary>すべての音を一時停止させる</summary>
    public void PauseAll()
    {
        PauseBGM();
        for (int i = 0; i < _seData.Count; i++)
        {
            PauseSE(i);
        }
    }
    
    /// <summary>一時停止していた音を再開させる</summary>
    public void ResumeAll()
    {
        ResumeBGM();
        for (int i = 0; i < _seData.Count; i++)
        {
            ResumeSE(i);
        }
    }    

#endregion


#region "BGMの再生と停止に関する処理"

    /// <summary>BGMを開始する</summary>
    /// <param name="cueName">流したいキューの名前</param>
    /// <param name="volume">音量</param>
    public void PlayBGM(string cueName, float volume = 1f)
    {
        var newBGMData = new CriPlayerData();
        var cueSheet = CriAtom.GetCueSheet(_cueSheetBGM);
        
        if (cueSheet == null)
        {
            Debug.LogError($"Cue sheet {_cueSheetBGM} が見つかりません.");
            return;
        }

        var tempAcb = cueSheet.acb;
        
        if (tempAcb == null)
        {
            Debug.LogError("ACBがnullです。 BGMが再生できません。");
            return;
        }        
        
        // 同じBGMを再生しようとした時は何もしない
        if (_currentBGMAcb == tempAcb && _currentBGMCueName == cueName &&
            _bgmPlayer.GetStatus() == CriAtomExPlayer.Status.Playing)
        {
            return;
        }

        StopBGM();  // 曲セレクト時にプレビューを流す場合は変更が必要

        if (newBGMData.IsLoop)
        {
            _bgmLoopPlayer.SetCue(tempAcb, cueName);
            _bgmLoopPlayer.SetVolume(volume * _bgmVolume * _masterVolume);
            newBGMData.Playback = _bgmLoopPlayer.Start();
        }
        else
        {
            _bgmPlayer.SetCue(tempAcb, cueName);
            _bgmPlayer.SetVolume(volume * _bgmVolume * _masterVolume);
            _bgmData.Playback = _bgmPlayer.Start();
        }
        _currentBGMAcb = tempAcb;
        _currentBGMCueName = cueName;    
    }
    
    /// <summary>BGMを中断させる</summary>
    public void PauseBGM()
    {
        // if文いらないかも？
        if (_bgmPlayer.GetStatus() == CriAtomExPlayer.Status.Playing)
        {
            _bgmPlayer.Pause();
        }
        if (_bgmLoopPlayer.GetStatus() == CriAtomExPlayer.Status.Playing)
        {
            _bgmLoopPlayer.Pause();
        }
    }

    /// <summary>中断したBGMを再開させる</summary>
    public void ResumeBGM()
    {
        _bgmPlayer.Resume(CriAtomEx.ResumeMode.PausedPlayback);
        _bgmLoopPlayer.Resume(CriAtomEx.ResumeMode.PausedPlayback);
    }

    /// <summary>BGMを停止させる</summary>
    public void StopBGM()
    {
        // if文いらないかも？
        if (_bgmPlayer.GetStatus() == CriAtomExPlayer.Status.Playing)
        {
            _bgmPlayer.Stop();
        }
        if (_bgmLoopPlayer.GetStatus() == CriAtomExPlayer.Status.Playing)
        {
            _bgmLoopPlayer.Stop();
        }           
    }

#endregion


#region "SEの再生と停止に関する処理"

    /// <summary>SEを流す</summary>
    /// <param name="cueName">流したいキューの名前</param>
    /// <param name="volume">音量</param>
    /// <returns>停止する際に必要なIndex</returns>
    public int PlaySE(string cueName, float volume = 1f)
    {
        CriPlayerData newSEData = new CriPlayerData();
        var cueSheet = CriAtom.GetCueSheet(_cueSheetSE);
        
        if (cueSheet == null)
        {
            Debug.LogError($"Cue sheet {_cueSheetBGM} が見つかりません.");
            return -1;
        }
        
        var tempAcb = cueSheet.acb;
        
        if (tempAcb == null)
        {
            Debug.LogWarning("ACBがNullです。");
            return -1;
        }
        
        _sePlayer.SetCue(tempAcb, cueName);
        _sePlayer.SetVolume(volume * _seVolume * _masterVolume);
        newSEData.Playback = _sePlayer.Start();

        _seData.Add(newSEData);
        return _seData.Count - 1;
    }
    
    //　長いSEを使わない場合停止・再開はなくてもいい
    
    /// <summary>SEをPauseさせる</summary>
    /// <param name="index">一時停止させたいPlaySE()の戻り値 (-1以下を渡すと処理を行わない)</param>
    public void PauseSE(int index)
    {
        if (index < 0) return;
        
        // if文いらないかも？
        if (_sePlayer.GetStatus() == CriAtomExPlayer.Status.Playing)
        {
            _seData[index].Playback.Pause();
        }
    }

    /// <summary>PauseさせたSEを再開させる</summary>
    /// <param name="index">再開させたいPlaySE()の戻り値 (-1以下を渡すと処理を行わない)</param>
    public void ResumeSE(int index)
    {
        if (index < 0) return;

        _seData[index].Playback.Resume(CriAtomEx.ResumeMode.PausedPlayback);
    }

    /// <summary>SEを停止させる </summary>
    /// <param name="index">止めたいPlaySE()の戻り値 (-1以下を渡すと処理を行わない)</param>
    public void StopSE(int index)
    {
        if (index < 0) return;

        // if文いらないかも？
        if (_sePlayer.GetStatus() == CriAtomExPlayer.Status.Playing)
        {
            _seData[index].Playback.Stop();
        }
    }

#endregion

    // リソースの解放
    // ずっとシーン上に置いとくならいらないかも
    /*private void Unload(Scene scene)
    {
        StopLoopSE();

        var removeIndex = new List<int>();
        for (int i = _seData.Count - 1; i >= 0; i--)
        {
            if (_seData[i].Playback.GetStatus() == CriAtomExPlayback.Status.Removed)
            {
                removeIndex.Add(i);
            }
        }

        foreach (var i in removeIndex)
        {
            _seData.RemoveAt(i);
        }
    }*/
}
