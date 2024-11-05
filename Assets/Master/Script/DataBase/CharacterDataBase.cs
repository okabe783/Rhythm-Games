using UnityEngine;

/// <summary>CharacterDataの管理</summary>
[CreateAssetMenu(menuName = "Scriptable / Character")]
public class CharacterDataBase : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _faceIcon;
    [SerializeField] private GameObject _character;
    [SerializeField] private int _characterId;
    [SerializeField] private int _hp;
    [SerializeField, Header("スキル名")] private string _skillName;
    [SerializeField, Header("スキルの説明")][TextArea(1, 4)] private string _skillInfo;

    public Sprite Icon => _icon;
    public Sprite FaceIcon => _faceIcon;
    public GameObject Character => _character;
    public int CharacterId => _characterId;
    public int Hp => _hp;
    public string SkillName => _skillName;
    public string SkillInfo => _skillInfo;
}