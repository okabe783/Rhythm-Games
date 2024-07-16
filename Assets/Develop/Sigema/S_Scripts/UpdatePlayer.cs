using UnityEngine;

public class UpdatePlayer : MonoBehaviour
{
    [SerializeField,Header("データを保存しておく箱")] private SelectData _selectData;
    [SerializeField,Header("キャラのリスト")] private CharacterDataBaseList _characterDataBaseList;

    #region 読み込むデータ変数
    
    private Sprite _sprite;
    private int _hp;
    
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
            _hp = selectedCharacterData.Hp;
            
            //SpriteやHpの更新処理を追加
            UpdateVisuals();
        }
    }

    private void UpdateVisuals()
    {
        //ここに読み込んできたデータを使用したViewをつくる
        //別のView用のクラスに書いてもOK
    }
}