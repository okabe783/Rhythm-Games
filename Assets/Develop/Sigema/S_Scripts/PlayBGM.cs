using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    [SerializeField] private string _bgmName = default;
    [SerializeField] private float _volume = 1f;
    
    private void Start()
    { 
        CriSoundManager.Instance.PlayBGM(_bgmName, _volume);
    }
}