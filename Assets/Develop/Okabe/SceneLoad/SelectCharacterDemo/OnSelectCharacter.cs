using UnityEngine;

/// <summary>Buttonにセット</summary>
public class OnSelectCharacter : MonoBehaviour
{
    [SerializeField] private int _selectId;

    /// <summary>Characterが選択された時Dataを保存してSceneを切り替える</summary>
    public void OnClickSelectCharacter()
    {
        SceneLoad.Instance.StartLongLoad("SampleInGame");
    }
}
