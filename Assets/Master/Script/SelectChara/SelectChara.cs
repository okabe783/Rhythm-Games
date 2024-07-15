using UnityEngine;

/// <summary>
/// ボタンクリック時にキャラの情報を保存クラスに保存し、シーンをロードする
/// </summary>
public class SelectChara : MonoBehaviour
{
    /// <summary>
    /// IDをボタンで設定する必要がある
    /// </summary>
    /// <param name="id"></param>
    public void OnClick(int id)
    {
        // todo: 保存クラスに保存
        //
        Debug.Log($"キャラID : {id} を保存します。");
        // todo: 保存クラスからロードするシーン名を受け取る
        //
        //SceneLoad.Instance.StartLongLoad("***");
    }
}