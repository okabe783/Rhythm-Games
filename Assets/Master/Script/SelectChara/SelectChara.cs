using UnityEngine;

/// <summary>
/// ボタンクリック時にキャラの情報を保存クラスに保存し、シーンをロードする
/// </summary>
public class SelectChara : MonoBehaviour
{
    [SerializeField] private SelectData _selectData; 
    
    /// <summary>
    /// IDをボタンで設定する必要がある
    /// </summary>
    /// <param name="id"></param>
    public void OnClick(int id)
    {
        // todo: 保存クラスに保存
        _selectData._characterId = id;
        Debug.Log($"キャラID : {id} を保存します。");
        // todo: 保存クラスからロードするシーン名を受け取る
        var sceneName = _selectData.ItemData.SelectScene;
        Debug.Log(sceneName);

        SceneLoad.Instance.StartLongLoad(sceneName,"SelectChara");
    }
}