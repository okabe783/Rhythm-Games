using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ボタンクリック時にキャラの情報を保存クラスに保存し、シーンをロードする
/// </summary>
public class SelectChara : MonoBehaviour
{
    [SerializeField] private SelectData _selectData = default;
    [SerializeField] private string _seName = default;
    [SerializeField, Header("待機時間")] private float _wfs = 5f; // 決定SEとキャラSEを合わせての長さ
    [SerializeField, Header("キャラID")] private int _id = default;
    [SerializeField] private Image[] _images = default; // レイキャストターゲットを変更
    private Button _button = default;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => OnClick(_id));
    }

    /// <summary>
    /// IDをボタンで設定する必要がある
    /// </summary>
    /// <param name="id"></param>
    public void OnClick(int id)
    {
        // 押されたときにSE再生
        CriSoundManager.Instance.PlaySE(_seName);
        _selectData._characterId = id; // 保存クラスに保存
        Debug.Log($"キャラID : {id} を保存します。");
        SleepButton(id);
        StartCoroutine(WaitForSe());
    }

    /// <summary>
    /// SEの再生が終わったころにシーンを遷移する。
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForSe()
    {
        yield return new WaitForSeconds(_wfs);
        var sceneName = _selectData.ItemData.SelectScene; // 保存クラスからロードするシーン名を受け取る
        Debug.Log($"{sceneName}へ遷移します。");
        CriSoundManager.Instance.StopBGM();
        SceneLoad.I.StartLongLoad(sceneName, "SelectCharacter");
    }

    /// <summary>
    /// 選ばれたキャラ以外の、
    /// かざしたときの機能を無効化する
    /// </summary>
    private void SleepButton(int id)
    {
        for (var i = 0; i < _images.Length; i++)
        {
            if (i == id) continue;
            _images[i].raycastTarget = false;
        }
    }
}