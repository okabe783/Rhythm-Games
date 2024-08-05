using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YInputScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //_ruleBook.Perfect();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            //_ruleBook.Great();
        }
    }
}
