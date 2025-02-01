using UnityEngine;

public class RateGenerate : MonoBehaviour
{
    [SerializeField] private GameObject _perfect;
    [SerializeField] private GameObject _great;
    [SerializeField] private GameObject _miss;

    private GameObject _perfectObj;
    private GameObject _greatObj;
    private GameObject _missObj;

    private void Start()
    {
        _perfectObj = Instantiate(_perfect, transform);
        _greatObj = Instantiate(_great, transform);
        _missObj = Instantiate(_miss, transform);
    }

    public void Rate(Rating rate)
    {
        switch (rate)
        {
            case Rating.Perfect :
                _perfectObj.SetActive(true);
                break;
            case Rating.Great :
                _greatObj.SetActive(true);
                break;
            case Rating.Miss :
                _missObj.SetActive(true);
                break;
        }
    }
}
