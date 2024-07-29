using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObject : MonoBehaviour
{
    Animator animator;

    [SerializeField] string animationString;

    private void Start() {
        animator = gameObject.GetComponent<Animator>();    
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A) && animationString != "")
            PlaySetAnimaiton();
    }

    public void PlayAnimaiton(string animation) {
        animator.Play(animation);
    }

    public void PlaySetAnimaiton() {
        animator.Play(animationString);
    }
}
