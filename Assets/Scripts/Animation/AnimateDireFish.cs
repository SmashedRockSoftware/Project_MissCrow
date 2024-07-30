using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimateDireFish : MonoBehaviour {
    Animator animator;

    const string UPSET1 = "ArmatureDirefish|Direfishidlein";
    const string CALM1 = "ArmatureDirefish|Direfishfall";

    bool isUpset = true;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (!isUpset) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime >= 1.0f)
            animator.Play(UPSET1);
    }

    public void MakeCalm() {
        isUpset = false;
        animator.Play(CALM1);
    }
}


