using System;
using System.Collections.Generic;
using UnityEngine;

public class SetRank : MonoBehaviour
{
    // 仮
    //[SerializeField] private SelectData _selectData;
    [SerializeField, Header("スコアの保存場所")] private TestSaveData _testSaveData;
    // 降順でランクイメージを入れる
    [SerializeField, Header("ランクスタンプ(降順)")] private List<GameObject> _rankStamps;

    private void Start()
    {
        // 仮
        SetRankStamp();
    }

    /// <summary>
    /// スコアに応じてスタンプを表示する
    /// ToDO:スコア等のアニメーションが終わったとき呼び出す
    /// </summary>
    public void SetRankStamp()
    {
        // スコアの基準は仮
        if (_testSaveData.Score >= 600)      // S
        {
            Debug.Log("ランクS");
            _rankStamps[0].SetActive(true);
        }
        else if (_testSaveData.Score >= 400) // A
        {
            Debug.Log("ランクA");
            _rankStamps[1].SetActive(true);
        }
        else if (_testSaveData.Score >= 200) // B
        {
            Debug.Log("ランクB");
            _rankStamps[2].SetActive(true);
        }
        else                                 // C
        {
            Debug.Log("ランクC");
            _rankStamps[3].SetActive(true);
        }
    }
}
