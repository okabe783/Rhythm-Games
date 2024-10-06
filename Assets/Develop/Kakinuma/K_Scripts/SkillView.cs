using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField, Header("スキルのスライダー")] private Slider _skillGauge;

    /// <summary> 上限をスライダーに反映する </summary>
    /// <param name="skillGaugeLimit"></param>
    public void SetSkillGaugeLimit(float skillGaugeLimit)
    {
        _skillGauge.maxValue = skillGaugeLimit;
    }

    /// <summary> 現在のスキルポイントをスライダーに反映する </summary>
    /// <param name="skillPoint"></param>
    public void SetSkillPoint(float skillPoint)
    {
        _skillGauge.value = skillPoint;
    }
}
