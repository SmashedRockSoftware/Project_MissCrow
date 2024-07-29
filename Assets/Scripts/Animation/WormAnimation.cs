using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WormAnimation : MonoBehaviour {
    Animator animator;

    const string UPSET1 = "Armaturewierworm1|wierwormupset1-100";
    const string UPSET2 = "Armaturewierworm1|wierwormupset2-100";
    const string CALM1 = "Armaturewierworm1|wierwormcalm1-100";
    [SerializeField] int randomSplit = 50;

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
            animator.Play(ChooseRandomIdle());
    }

    public void MakeCalm() {
        isUpset = false;
        animator.Play(CALM1);
    }

    private string ChooseRandomIdle() {
        int random = Random.Range(0, 100);

        if (random < randomSplit)
            return UPSET2;
        else
            return UPSET1;
    }
}

