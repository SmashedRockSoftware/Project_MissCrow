using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SunRayAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] MeshRenderer sunRay;
    [SerializeField] ParticleSystem pSystem;

    bool hasPlayed = false;

    const string activeName = "Sunray1|Sunrayactivate-220";

    // Start is called before the first frame update
    void Start() {
        //animator = GetComponent<Animator>();
    }

    public void ShowSunRay() {
        sunRay.gameObject.SetActive(true);
        hasPlayed = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!hasPlayed) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        pSystem.gameObject.SetActive(stateInfo.IsName(activeName));
        sunRay.enabled = stateInfo.IsName(activeName);

        //enabled = false;
    }
}
