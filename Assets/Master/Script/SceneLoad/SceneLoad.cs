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

    private AsyncOperation _async;　// ロードの進捗状況を管理するための変数

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Additive); //複数シーンを同時に開く
    }

    public void OnChangeScene(string sceneName,string unloadScene)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(unloadScene);
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

//InGameシーンをロードするときはこっちを使う
    private IEnumerator InGameLoad(string sceneName)
    {
        var setUI = Instantiate(_loadingUI); //Uiを表示
        _async = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);

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