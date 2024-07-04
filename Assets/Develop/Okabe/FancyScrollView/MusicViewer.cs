using System.Linq;
using UnityEngine;

public class MusicViewer : MonoBehaviour
{
    [SerializeField] private MusicScrollView _musicScrollView;

    private void Start()
    {
        //Uiを表示
        var items = Enumerable.Range(0, 3).Select(i =>
            new MusicItemData(i, $"music{i:D2}")).ToArray();
        
        _musicScrollView.UpdateData(items);
    }
}
