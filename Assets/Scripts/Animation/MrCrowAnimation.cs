using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MrCrowAnimation : MonoBehaviour {
    Animator animator;

    //const string IDLE0 = "Armature_001|Mrcrowidle1-100";
    //const string IDLE1 = "Armature_001|Mrcrowidle2-100";
    //const string IDLE2 = "Armature_001|Mrcrowidle3-100";
    //const string IDLE3 = "Armature_001|Mrcrowidle4-100";

    const string IDLE0 = "idle1";
    const string IDLE1 = "idle2";
    const string IDLE2 = "idle3";
    const string IDLE3 = "idle4";

    const string CALM1 = "Armaturewierworm1|wierwormcalm1-100";
    private const string TALK = "Armature_001|Mrcrowtalk-100";
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

        if (stateInfo.IsName(TALK)) return;

        if (stateInfo.normalizedTime >= 1.0f)
            animator.SetTrigger(ChooseRandomIdle());
    }

    public void MakeCalm() {
        isUpset = false;
        animator.Play(CALM1);
    }

    private string ChooseRandomIdle() {
        int random = Random.Range(0, 100);

        if (random > 0 && random < 25)
            return IDLE0;
        if (random > 25 && random < 50)
            return IDLE1;
        if (random > 50 && random < 75)
            return IDLE2;
        else
            return IDLE3;
    }
}




