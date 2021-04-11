using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRegistrator : MonoBehaviour
{    
    private void Awake()
    {
        GameManager.GetInstance().GroundTransform = transform;       
    }
}
