using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObject : MonoBehaviour
{
    Animator animator;

    private void Start() {
        animator = gameObject.GetComponent<Animator>();    
    }

    public void PlayAnimaiton(string animation) {
        animator.Play(animation);
    }
}
