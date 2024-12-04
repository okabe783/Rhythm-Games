using System.Collections;
using UnityEngine;

public class RateObject : MonoBehaviour
{
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(WaitAnimation());
    }

    private IEnumerator WaitAnimation()
    {
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
