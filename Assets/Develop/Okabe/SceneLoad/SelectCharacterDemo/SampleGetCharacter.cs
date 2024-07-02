using System;
using UnityEngine;

public class SampleGetCharacter : MonoBehaviour
{
    private int _loadData;

    private void Start()
    {
        //リスタート前に保存したDataはけして
        _loadData = PlayerPrefs.GetInt("Character");
    }
}
