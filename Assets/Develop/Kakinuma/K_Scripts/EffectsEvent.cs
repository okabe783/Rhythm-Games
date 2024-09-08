using System.Collections.Generic;
using UnityEngine;

public class EffectsEvent : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _effects;

    public void OnPlayEffect(int index)
    {
        if (index >= _effects.Count)
        {
            Debug.LogWarning("指定のエフェクトのインデックスが無効です");
            return;
        }
        _effects[index].Play();   
    }
}
