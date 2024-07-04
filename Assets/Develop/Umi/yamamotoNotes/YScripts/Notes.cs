using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    void Update()
    {
        transform.position -= transform.right * (10f * Time.deltaTime);
    }
}
