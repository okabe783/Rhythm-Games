using UniRx;
using UnityEngine;

public class SkillModel : MonoBehaviour
{
    // デフォルト設定の場合:パーフェクト50回でスキル発動、効果時間10秒
    // ToDo:キャラクターデータからスキルの追加率等を持ってくる(あれば)
    [SerializeField, Header("ポイント追加割合 (ポイント×Add Rate)")] private float _addRate = 1.0f;
    [SerializeField, Header("１秒で減るポイント量")] private float _difRate = 10.0f;
    [SerializeField, Header("パーフェクトで増えるポイント量")] private const float _perfectPoint = 2.0f;
    [SerializeField, Header("グレートで増えるポイント量")]private const float _greatPoint = 1.0f;
    
    [SerializeField, Header("スキルゲージの上限")] private float _skillGaugeLimit = 100.0f;

    private Skill _skillState = Skill.Inactive;
    private ReactiveProperty<float> _currentSkillPoint = new ReactiveProperty<float>(0);

    public float SkillGaugeLimit => _skillGaugeLimit;
    public IReadOnlyReactiveProperty<float> CurrentSkillPoint => _currentSkillPoint;
    
    private void Update()
    {
        ChangeSkillState();
        if (_skillState == Skill.Inactive)
        {
            // 判定によって追加ポイントを変更
            JudgeAddSkillPoint();
        }
        else
        {
            MinusCurrentSkillValue();
        }
    }

    // ポイントを追加する
    // (キャラ毎にスキルポイント追加率を設定したり
    // パーフェクト、グレートでポイントを分ける場合を想定)
    private void PlusCurrentSkillValue(float point)
    {
        _currentSkillPoint.Value += point * _addRate;
    }

    // ポイントを減少(使用)する
    // (キャラ毎にスキル効果時間を設定する場合を想定)
    private void MinusCurrentSkillValue()
    {
        _currentSkillPoint.Value -= Time.deltaTime * _difRate;
    }

    // 判定によってスキルゲージに値を追加する
    // ToDo:引数に判定をもらう？
    private void JudgeAddSkillPoint()
    {
        // 判定がパーフェクトのとき(仮)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 固定値 × キャラ毎のスコア追加量
            PlusCurrentSkillValue(_perfectPoint);
        }
        // 判定がグレートの時(仮)
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlusCurrentSkillValue(_greatPoint);
        }
    }

    private void ChangeSkillState()
    {
        // ゲージが0になったらスキル解除
        if (_currentSkillPoint.Value < 0f)
        {
            _currentSkillPoint.Value = 0f;
            _skillState = Skill.Inactive;
        }
        // ゲージがMAXになったらスキル発動
        else if (_currentSkillPoint.Value >= _skillGaugeLimit)
        {
            _skillState = Skill.Active;
        }
    }
}
