using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationShuffler : MonoBehaviour
{

    [SerializeField] private AnimationClip[] animationPool;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        AnimationClip randomAnimation = animationPool[Random.Range(0, animationPool.Length)];
        animator.Play(randomAnimation.name);
    }



    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            AnimationClip randomAnimation = animationPool[Random.Range(0, animationPool.Length)];
            animator.Play(randomAnimation.name);
        }
    }
}
