using UnityEngine;
using UniRx;

public enum Rating
{
    Perfect,
    Great,
    Miss
}

/// <summary> スコア </summary>
public class Score : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private ComboManager _comboManager;

    [SerializeField, Header("Perfectの加算ポイント")] private int _perfectPoint;
    
    [SerializeField, Header("Greatの加算ポイント")] private int _greatPoint;

    [SerializeField, Header("何コンボから表示するか")] private int _displayedCombo;
    
    private IntReactiveProperty _score = new IntReactiveProperty(0);
    private IntReactiveProperty _combo = new IntReactiveProperty(0);
    
    private int _perfect = 0;
    private int _great = 0;
    private int _miss = 0;
    
    private void Start()
    {
        _score.Value = 0;
        _score.Subscribe(x => _scoreManager.SetScore(x.ToString())).AddTo(this);
        _combo.Value = 0;
        _combo.Subscribe(x => _comboManager.SetCombo(x.ToString())).AddTo(this);
        _combo.Subscribe(x => _comboManager.ComboVisibility(x >= _displayedCombo)).AddTo(this);
    }

    public void AddScore(Rating _rating)
    {
        switch (_rating)
        {
            case Rating.Perfect:
                _score.Value += _perfectPoint;
                _combo.Value++;
                _perfect++;
                break;
            case Rating.Great:
                _score.Value += _greatPoint;
                _combo.Value++;
                _great++;
                break;
            case Rating.Miss:
                _combo.Value = 0;
                _miss++;
                break;
        }
    }
}
