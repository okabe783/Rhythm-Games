using UnityEngine;
using UniRx;

public class HPPresenter : MonoBehaviour
{
    [SerializeField] private UpdatePlayer _model = default;
    [SerializeField] private HPView _view = default;

    private void Awake()
    {
        _model.OnInitialHpSet += SetSliderInitialValue;
    }

    /// <summary>
    /// HPの初期化が終わったら実行される
    /// </summary>
    private void SetSliderInitialValue()
    {
        _view.SetMaxHp(_model.CurrentHp.Value);
        _model.CurrentHp
            .Subscribe(hp => _view.SetSliderValue(hp))
            .AddTo(this);
    }

    public void Damage(int amount)
    {
        _model.Damage(amount);
    }
}