using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>Sceneを遷移させる</summary>
public class SceneLoad : MonoBehaviour
{
    //シングルトン化
    public static SceneLoad Instance;
    
    [SerializeField, Header("ロード中に表示するUI")]
    private GameObject _loadingUI;

    private AsyncOperation async;　// ロードの進捗状況を管理するための変数

    private void Awake()
    {
        Instance = this;
    }

    // ロードを開始するメソッド
    public void StartLoad(string sceneName)
    {
        StartCoroutine(Load(sceneName));
    }

    private IEnumerator Load(string sceneName)
    {
        _loadingUI.SetActive(true); //ロード画面を表示
        async = SceneManager.LoadSceneAsync(sceneName);

        // ロードが完了するまで待機する
        while (!async.isDone)
        {
            yield return null;
        }

        _loadingUI.SetActive(false); // ロード画面を非表示にする
    }
}