using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimateOnMouse : MonoBehaviour
{
    [SerializeField] Transform floatingObject;
    [SerializeField] Transform glisteningTransform;

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

    [SerializeField] Renderer glisteningBackground;
    [SerializeField] float maxAlpha = 1f;

    // Start is called before the first frame update
    void Start() {
        startingPosition = floatingObject.position;
        startingRotation = floatingObject.rotation.eulerAngles;
        startingScale = floatingObject.localScale;

        glisteningStartPos = glisteningTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnMouseEnter() {
        glisteningTransform.DOBlendableLocalMoveBy(localMovement, duration).SetEase(ease);

        floatingObject.DOBlendableLocalMoveBy(localMovement, duration).SetEase(ease);
        floatingObject.DORotate(localRotation, duration).SetEase(ease);

        glisteningBackground.material.DOFade(maxAlpha, duration);
    }


    void OnMouseOver() {
        if(Input.GetMouseButtonDown(0)) {
            floatingObject.DOPunchScale(punchScale, punchDuration);
        }
    }


    void OnMouseExit() {
        glisteningTransform.DOMove(glisteningStartPos, duration).SetEase(ease);

        floatingObject.DOMove(startingPosition, duration).SetEase(ease);
        floatingObject.DORotate(startingRotation, duration).SetEase(ease);
        floatingObject.DOScale(startingScale, duration).SetEase(ease);

        glisteningBackground.material.DOFade(0f, duration);
    }
}
