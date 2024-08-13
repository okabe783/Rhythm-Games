using UnityEngine;

public class ImageAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void RudderSound()
    {
        CriSoundManager.Instance.PlaySE("SE_Kishimuoto");
    }
}
