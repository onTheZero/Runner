using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorAnimationStart : MonoBehaviour
{
    [SerializeField] private float animationStartOffset;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        animator.speed = 0.25f;
        animator.Play("ConveyorBelt", 0, animationStartOffset);
    }
}
