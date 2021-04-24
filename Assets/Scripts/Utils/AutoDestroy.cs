using System.Collections;
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
            StartCoroutine(ScaleDown(1f));        
            Destroy(gameObject, 1f);
        }
    }

    IEnumerator ScaleDown(float duration)
    {
        Vector3 startScale = transform.localScale;

        float progress = 0;

        GetComponent<Rigidbody2D>().gravityScale = 0f;

        while(progress <= duration)
        {
            progress = Mathf.Min(progress + Time.fixedDeltaTime, duration);
            transform.localScale = startScale * (duration - progress);
            yield return new WaitForEndOfFrame();
        }

    }

}
