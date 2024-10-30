using System;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class UpdatePlayer : MonoBehaviour, IDamage
{
    [SerializeField, Header("データを保存しておく箱")]
    private SelectData _selectData = default;

    [SerializeField, Header("キャラのリスト")] private CharacterDataBaseList _characterDataBaseList = default;
    [SerializeField, Header("再生するSE")] private string _seName = default;
    private IntReactiveProperty _currentHp = new(1);
    
    public IReadOnlyReactiveProperty<int> CurrentHp => _currentHp;
    public event Action OnInitialHpSet = default; // 初期化後に発火
    
    #region 読み込むデータ変数

    private Sprite _sprite = default;
    private int _maxHp = default;

    #endregion

    private void Start()
    {
        //Dataを読み込む
        var characterId = _selectData._characterId;

        CharacterDataBase selectedCharacterData = null;

        foreach (var characterData in _characterDataBaseList.CharacterDataBase)
        {
            if (characterData.CharacterId == characterId)
            {
                selectedCharacterData = characterData;
                break;
            }
        }

        if (selectedCharacterData != null)
        {
            //Dataの取得
            _sprite = selectedCharacterData.Icon;
            _maxHp = selectedCharacterData.Hp;
            //SpriteやHpの更新処理を追加
            UpdateVisuals();
            OnInitialHpSet?.Invoke();
        }
    }

    private void UpdateVisuals()
    {
        //ここに読み込んできたデータを使用したViewをつくる
        //別のView用のクラスに書いてもOK
        _currentHp.Value = _maxHp;
        GetComponent<Image>().sprite = _sprite;
    }

    public void Damage(int value)
    {
        if (_seName != string.Empty) CriSoundManager.Instance.PlaySE(_seName);
        if (_currentHp.Value < 0)
        {
            _currentHp.Value = 0;
            return;
        }

        _currentHp.Value -= value;
    }
}