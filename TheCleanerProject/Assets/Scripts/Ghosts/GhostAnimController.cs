using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimController : MonoBehaviour
{
    Animator animator;
    string currentAnimation;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string name)
    {
        if (currentAnimation == name) return;
        
        animator.Play(name);
        currentAnimation = name;
    }
}