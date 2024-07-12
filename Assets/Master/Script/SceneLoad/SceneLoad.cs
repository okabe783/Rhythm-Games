using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

/// <summary>Sceneを遷移させる</summary>
public class SceneLoad : MonoBehaviour
{
    //シングルトン化
    public static SceneLoad Instance;

    [SerializeField, Header("ロード中に表示するUI")]
    private GameObject _loadingUI;

    private AsyncOperation _async;　// ロードの進捗状況を管理するための変数

    //ToDo:UniRxで依存せずにsceneを呼び出す

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // sceneLoadHandler.Subscribe(StartShortLoad).AddTo(this);
        // SceneManager.sceneLoaded += NewMethod;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= NewMethod;
    }

    private void NewMethod(Scene scene, LoadSceneMode mode)
    {
        // シーンが読み込まれたときに呼び出される。
    }

    // Loading中にUIを表示するメソッド
    public void StartLongLoad(string sceneName)
    {
        StartCoroutine(InGameLoad(sceneName));
    }

    /// <summary>UIを表示しないシーンのロード</summary>
    public void StartShortLoad(string sceneName)
    {
        NormalLoading(sceneName);
    }

    //非同期でシーンをロードする
    private IEnumerator InGameLoad(string sceneName)
    {
        var setUI = Instantiate(_loadingUI); //Uiを表示
        _async = SceneManager.LoadSceneAsync(sceneName);

        // ロードが完了するまで待機する
        while (!_async.isDone)
        {
            yield return null;
        }

        Destroy(setUI);
    }

    //InGameに突入する以外のシーンの遷移はこっちで行う
    private void NormalLoading(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}