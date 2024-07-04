using UnityEngine;

/// <summary>CharacterDataの管理</summary>
[CreateAssetMenu(menuName = "Scriptable / Character")]
public class CharacterDataBase : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _characterId;
    [SerializeField] private int _hp;

    public Sprite Icon => _icon;
    public int CharacterId => _characterId;
    public int Hp => _hp;
}
