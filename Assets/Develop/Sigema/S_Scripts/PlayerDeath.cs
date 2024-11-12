using System.Collections;
using UnityEngine;

/// <summary>
/// HP=0で入力制限
/// GameOverパネルをアニメーションさせ、GameOverシーンへ遷移する
/// </summary>
public class PlayerDeath : MonoBehaviour
{
    #region 変数

    [SerializeField, Header("ゲームオーバーシーンの名前")]
    private string _nameOfGameOverScene = default;

    [SerializeField, Header("死亡アニメーションの時間")]
    private float _waitTime = 3f;

    [SerializeField, Header("キャンバスのAnim")] private Animator _animator = default;

    [SerializeField, Header("AnimのState名")]
    private string _stateName = default;

    private PlayerInput _playerInput = default;
    private UpdatePlayer _updatePlayer = default;
    private PlayerAnimation _playerAnimation = default;
    private bool _isDead = default;
    private bool _canPlayDeath = true;
    private WaitForSeconds _wfs = default;

    #endregion

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _updatePlayer = GetComponent<UpdatePlayer>();
        _playerAnimation = FindObjectOfType<PlayerAnimation>();
        _wfs = new WaitForSeconds(_waitTime);
    }

    private void Update()
    {
        _isDead = _updatePlayer.CurrentHp.Value <= 0;
        if (!_isDead) return;
        if (_canPlayDeath) StartCoroutine(LatePlay());
    }

    private IEnumerator LatePlay()
    {
        if (_canPlayDeath) _playerAnimation.PlayDeath();
        _playerInput.enabled = false;
        _canPlayDeath = false;
        Debug.Log("プレイヤーが死亡  キー入力制限");

        yield return _wfs;

        if (_animator && _stateName != "")
        {
            _animator.Play(_stateName);
        }

        yield return new WaitForSeconds(1f);

        if (_nameOfGameOverScene != "")
        {
            SceneLoad.I.OnChangeScene(_nameOfGameOverScene, "MainStage");
            Debug.Log("GameOverシーンへ遷移");
        }

        // 曲停止
        CriSoundManager.Instance.StopBGM();
    }
}