using UniRx;
using UnityEngine;

public class SkillPresenter : MonoBehaviour
{
    [SerializeField] private SkillView _skillView;
    [SerializeField] private SkillModel _skillModel;
    
    void Start()
    {
        _skillView.SetSkillGaugeLimit(_skillModel.SkillGaugeLimit);
        _skillModel.CurrentSkillPoint.Subscribe(_skillView.SetSkillPoint).AddTo(this);
    }
}
