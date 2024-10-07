using UnityEngine;
using UnityEngine.UI;

public class SelectMusicView : SingletonMonoBehaviour<SelectMusicView>
{
    [SerializeField] private Button _prevCellButton;
    [SerializeField] private Button _nextCellButton;

    public void DeactivateUI()
    {
        _prevCellButton.gameObject.SetActive(false);
        _nextCellButton.gameObject.SetActive(false);
    }

    public void ActiveUI()
    {
        _prevCellButton.gameObject.SetActive(true);
        _nextCellButton.gameObject.SetActive(true);
    }
}
