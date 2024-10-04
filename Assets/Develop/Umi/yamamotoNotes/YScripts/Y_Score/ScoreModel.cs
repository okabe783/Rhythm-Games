using System;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

public enum Rating
{
    Perfect,
    Great,
    Miss
}

/// <summary> スコア </summary>
public class ScoreModel : MonoBehaviour
{
    [SerializeField, Header("Perfectの加算ポイント")] private int _perfectPoint;
    [SerializeField, Header("Greatの加算ポイント")] private int _greatPoint;
    
    private IntReactiveProperty _score = new IntReactiveProperty();
    public IReadOnlyReactiveProperty<int> Score => _score;
    private IntReactiveProperty _combo = new IntReactiveProperty();
    public IReadOnlyReactiveProperty<int> Combo => _combo;
    
    private int _perfect = 0;
    private int _great = 0;
    private int _miss = 0;

    private void Start()
    {
        _score.Value = 0;
        _combo.Value = 0;
    }

    public void AddScore(Rating rating)
    {
        switch (rating)
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
