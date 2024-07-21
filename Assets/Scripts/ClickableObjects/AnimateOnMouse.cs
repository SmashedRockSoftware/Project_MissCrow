using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimateOnMouse : MonoBehaviour
{
    [Tooltip("Leave the floating object blank, so it wont float, should be the visual model, floats on mouse over")]
    [SerializeField] Transform floatingObject;

    [Tooltip("This is a quad that fades in with a circle texture, need the transform to make it float")]
    [SerializeField] Transform glisteningTransform;

    [Tooltip("The time for the float, glisten to occur")]
    [SerializeField] float duration = 1f;
    [SerializeField] Ease ease = Ease.InBounce;

    [SerializeField] Vector3 localMovement = Vector3.up;
    Vector3 startingPosition;
    Vector3 glisteningStartPos;

    [SerializeField] Vector3 localRotation = new Vector3(5f,15f,0);
    Vector3 startingRotation;

    [SerializeField] Vector3 punchScale = Vector3.one;
    [SerializeField] float punchDuration = 0.15f;
    Vector3 startingScale;

    [Space]
    [Tooltip("This is a quad that fades in with a circle texture, need the rend to make fade in and out")]
    [SerializeField] Renderer glisteningBackground;
    [SerializeField] float maxAlpha = 1f;

    // Start is called before the first frame update
    void Start() {
        if (floatingObject != null) {
            startingPosition = floatingObject.position;
            startingRotation = floatingObject.rotation.eulerAngles;
            startingScale = floatingObject.localScale;

            glisteningStartPos = glisteningTransform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnMouseEnter() {
        if (GameManager.Instance.currentGameState != GameState.InGame) return;



        if (floatingObject != null) {  //only move if we have a floating object
            glisteningTransform.DOBlendableLocalMoveBy(localMovement, duration).SetEase(ease);
            floatingObject.DOBlendableLocalMoveBy(localMovement, duration).SetEase(ease);
            floatingObject.DORotate(localRotation, duration).SetEase(ease);
        }

        glisteningBackground.material.DOFade(maxAlpha, duration);
    }


    void OnMouseOver() {
        if (GameManager.Instance.currentGameState != GameState.InGame) return;

        if (Input.GetMouseButtonDown(0) && floatingObject != null) {
            floatingObject.DOPunchScale(punchScale, punchDuration);
        }
    }


    void OnMouseExit() {
        if (GameManager.Instance.currentGameState != GameState.InGame) return;

        if (floatingObject != null) {
            glisteningTransform.DOMove(glisteningStartPos, duration).SetEase(ease);
            floatingObject.DOMove(startingPosition, duration).SetEase(ease);
            floatingObject.DORotate(startingRotation, duration).SetEase(ease);
            floatingObject.DOScale(startingScale, duration).SetEase(ease);
        }

        glisteningBackground.material.DOFade(0f, duration);
    }
}
