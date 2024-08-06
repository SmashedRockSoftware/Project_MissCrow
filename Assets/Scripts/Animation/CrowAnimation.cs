using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrowAnimation : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;

    const string IDLE1 = "ArmatureCrow1|Crowidle1-100";
    const string IDLE2 = "ArmatureCrow1|Crowidle2-100";
    const string IDLEPOTION = "ArmaturePotion1|Crowidle4-200";
    const string IDLETOME = "ArmatureTome1|Crowidle4-200";

    const string WAKE1 = "ArmatureCrow1|Crowwakeup-200";

    const string WALK1 = "ArmatureCrow1|Crowwalk1-60";
    [SerializeField] private float minMoveSpeed = 0.5f;


    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponentInParent<NavMeshAgent>();
    }

    public void WakeUpAnimation() {
        animator.Play(WAKE1);
    }

    public void EndOpening() {
        FindAnyObjectByType<OpeningCutscne>()?.EndOpening();
    }

    public bool playingOpeningAnimation() {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        var isName = stateInfo.IsName(WAKE1);
        var isLength = (stateInfo.normalizedTime >= 1.0f);

        return isName || !isLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentGameState == GameState.InCutscene) return;

        //animator.SetFloat("agentSpeed", agent.velocity.magnitude);
        if(agent.velocity.magnitude < minMoveSpeed) {
            // Assuming the animation is in the first layer (index 0)
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // Check if the animation has looped
            if (stateInfo.normalizedTime >= 1.0f) { // stateInfo.loop && && !isClipChanged
                // Animation has completed a loop
                // Change the animation clip here
                animator.Play(ChooseRandomIdle());
            }
            else if (stateInfo.normalizedTime < 1.0f) {
            }
        }
        else
        {
            animator.Play(WALK1);
        }
    }

    private string ChooseRandomIdle() {
        int random = Random.Range(0, 100);

        if (random < 3)
            return IDLE2;
        else
            return IDLE1;
    }
}
