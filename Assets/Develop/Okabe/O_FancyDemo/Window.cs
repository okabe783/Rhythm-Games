using FancyScrollView;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    [SerializeField] private SlideScreenTransition _transition;
    [SerializeField, Header("曲のID")] private int _musicID;
    [SerializeField, Header("決定Button")] private Button _selectButton;
    [SerializeField, Header("保存する場所")] private SelectData _selectData;

    [SerializeField, Header("サウンドテーブルリスト")]
    private SoundTableList _soundTableList;

    private void Start()
    {
        _selectButton.onClick.AddListener(OnClickSelectMusic);
    }

    public void In(MovementDirection direction) => _transition?.In(direction);
    public void Out(MovementDirection direction) => _transition?.Out(direction);

    private void OnClickSelectMusic()
    {
        for (int i = 0; i < _soundTableList.SoundTables.Count; i++)
        {
            if (_soundTableList.SoundTables[i].TableId != _musicID) continue;
            _selectData.ItemData = _soundTableList.SoundTables[i];
            Debug.Log($"Saved selected music data: {_selectData.ItemData.SoundName}");
            break;
        }
        //次のシーンに切り換える
        if (SceneLoad.Instance != null)
            SceneLoad.Instance.OnChangeScene("SelectCharacter", "SelectMusicScene");
        else
            Debug.LogError("SceneLoadインスタンスを取得できていません");
    }
}