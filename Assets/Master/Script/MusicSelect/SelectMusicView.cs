using UnityEngine;
using UnityEngine.UI;

public class SelectMusicView : SingletonMonoBehaviour<SelectMusicView>
{
    [SerializeField] private Button _prevCellButton;
    [SerializeField] private Button _nextCellButton;
    [SerializeField] private BackButton _backButton;

    public void DeactivateUI()
    {
        _prevCellButton.gameObject.SetActive(false);
        _nextCellButton.gameObject.SetActive(false);
        _backButton.gameObject.SetActive(false);
    }

    public void ActiveUI()
    {
        _prevCellButton.gameObject.SetActive(true);
        _nextCellButton.gameObject.SetActive(true);
        _backButton.gameObject.SetActive(true);
    }
}
