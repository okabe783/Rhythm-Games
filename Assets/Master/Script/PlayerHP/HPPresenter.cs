using UnityEngine;
using UniRx;

public class HPPresenter : MonoBehaviour
{
    [SerializeField] private UpdatePlayer _model = default;
    [SerializeField] private HPView _view = default;

    private void Start()
    {
        _view.SetMaxHp(_model.CurrentHp.Value);
        // モデルの現在のHP値が変化したときに、ビューを更新する
        _model.CurrentHp
            .Subscribe(hp => _view.SetSliderValue(hp))
            .AddTo(this);
    }

    public void Damage(int amount)
    {
        _model.Damage(amount);
    }
}