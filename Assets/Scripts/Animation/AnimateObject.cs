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
        if(animator == null) animator = gameObject.GetComponent<Animator>();
        animator.Play(animation);
    }

    public void PlaySetAnimaiton() {
        animator.Play(animationString);
    }

    public void SetScale(float scale) {
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
