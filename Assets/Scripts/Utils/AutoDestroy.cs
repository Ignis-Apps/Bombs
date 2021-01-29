using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float lifetime;
    [SerializeField] private bool useUnscaledTime;
    
    void Update()
    {
        if (useUnscaledTime)
        {
            lifetime -= Time.unscaledDeltaTime;
        }
        else
        {
            lifetime -= Time.deltaTime;
        }

        if(lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}
