using UniRx;
using UnityEngine;

public class ScorePresenter : MonoBehaviour
{
    [SerializeField] private ScoreModel _scoreModel;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private ComboManager _comboManager;
    
    [SerializeField, Header("何コンボから表示するか")] private int _displayedCombo;
    
    private void Start()
    {
        _scoreModel.Score.Subscribe(x => _scoreManager.SetScore(x.ToString())).AddTo(this);
        _scoreModel.Combo.Subscribe(x => _comboManager.SetCombo(x.ToString())).AddTo(this);
        _scoreModel.Combo.Subscribe(x => _comboManager.ComboVisibility(x >= _displayedCombo)).AddTo(this);
    }

    public void AddScore(Rating rating)
    {
        _scoreModel.AddScore(rating);
    }
}
