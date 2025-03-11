using UnityEngine;

/// <summary>
/// HPの上限を変更する
/// </summary>
[CreateAssetMenu(fileName = "HPUpperLimit", menuName = "Ability/HPUpperLimit")]
public class HPUpperLimit : ScriptableObject, IAbility
{
    [SerializeField, Header("上限HPに加算する")] private int _addHP = 10;

    [SerializeField, Header("UpdatePlayerをアタッチされたオブジェクトの名前")]
    private string _playerName;

    public void Ability()
    {
        var player = GameObject.Find(_playerName);
        var updatePlayer = player.GetComponent<UpdatePlayer>();
        updatePlayer.AddMaxHp = _addHP;
        // Debug.Log($"Ability HPUpperLimit add: {_addHP}");
    }
}