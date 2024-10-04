using UnityEngine;
using UnityEngine.UI;

public class OnClickCellButton:MonoBehaviour
{
    [SerializeField] private GameObject _musicSellPrefab;
    [SerializeField] private Button _musicCellButton;
    private void Start()
    {
        var sellButton = _musicCellButton.GetComponentInParent<Button>();
        sellButton.onClick.AddListener(OnClickMusicCell);
    }

    private void OnClickMusicCell()
    {
        var sellID = _musicSellPrefab.GetComponent<Tab>().MusicID;
        Debug.Log($"このCellのIDは{sellID}です");
    }
}