using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// キャラ選択シーン内で、アイコンなどのキャラ情報を設定する
/// </summary>
public class SetCharaInfo : MonoBehaviour
{
    [SerializeField] private CharacterDataBaseList _datalist = default;
    [SerializeField, Header("ボタンオブジェクト")] private List<GameObject> _gameObjects = default;
    [SerializeField, Header("HPの値を表示するテキスト名")] private string _textName = default;
    private List<Sprite> _sprites = default; // アイコン
    private List<int> _hps = default; // Hp

    private void Start()
    {
        if (_datalist.CharacterDataBase.Count != _gameObjects.Count)
            Debug.LogWarning($"{_datalist.name}要素の数と、{_gameObjects}要素の数が一致しません。");
        Initialized();
        GetInfo();
        SetInfo();
    }

    private void Initialized()
    {
        _sprites = new List<Sprite>();
        _hps = new List<int>();
    }

    private void GetInfo()
    {
        foreach (var data in _datalist.CharacterDataBase)
        {
            _sprites.Add(data.Icon);
            _hps.Add(data.Hp);
        }
    }

    private void SetInfo()
    {
        for (var i = 0; i < _gameObjects.Count; i++)
        {
            _gameObjects[i].GetComponent<Image>().sprite = _sprites[i];
            _gameObjects[i].transform.Find(_textName).GetComponent<Text>().text
                = _hps[i].ToString("000");
        }
    }
}