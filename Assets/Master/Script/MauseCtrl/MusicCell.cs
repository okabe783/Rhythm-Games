using FancyScrollView;
using UnityEngine;
using UnityEngine.UI;

/// <summary>音楽でデータを表示してセルの位置やイベントを発火する</summary>
public class MusicCell : FancyCell<MusicItemData,Context>
{
    [SerializeField,Header("セルのAnimation")] private Animator _animator;
    [SerializeField,Header("UIに表示するテキスト")] private Text _txtName;

    private string _sceneName; //選択された時に遷移させるシーン

    private float _currentPos;

    private static class AnimatorHash
    {
        public static readonly int _scroll = Animator.StringToHash("scroll");
    }
    
    //Objectを無効化するとAnimationがリセットされてしまうので現在の位置を再設定して対応する
    private void OnEnable() => UpdatePosition(_currentPos);

    //セルの内容を更新
    public override void UpdateContent(MusicItemData itemData)
    {
        //曲名をLoad
        _txtName.text = itemData.SoundName;
        _sceneName = itemData.SelectScene; //読み込むべきシーンをセルに登録
    }
    
    //セルの位置を更新
    public override void UpdatePosition(float position)
    {
        _currentPos = position;
        //Animが有効の場合
        if (_animator.isActiveAndEnabled)
        {
            _animator.Play(AnimatorHash._scroll,-1,position);
        }
            

        _animator.speed = 0;
    }
}