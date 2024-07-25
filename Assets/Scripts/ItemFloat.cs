using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemFloat : MonoBehaviour {
    [SerializeField] float duration = 1f;
    [SerializeField] Ease ease = Ease.InOutCirc;

    [SerializeField] Vector3 localMovement = new Vector3(0,0.2f,0);
    Vector3 startingPosition;

    [SerializeField] Vector3 localRotation = new Vector3(15f, 45f, 0);
    Vector3 startingRotation;

    [SerializeField] Vector3 punchScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] float punchDuration = 0.15f;
    Vector3 startingScale;

    [SerializeField] Transform floatingObject;


    // Start is called before the first frame update
    void Start() {
        if (floatingObject != null) {
            startingPosition = floatingObject.position;
            startingRotation = floatingObject.rotation.eulerAngles;
            startingScale = floatingObject.localScale;


        }
    }

    // Update is called once per frame
    void Update() {

    }


    void OnMouseEnter() {
        if (GameManager.Instance.currentGameState != GameState.InGame) return;



        if (floatingObject != null) {  //only move if we have a floating object

            floatingObject.DOBlendableLocalMoveBy(localMovement, duration).SetEase(ease);
            floatingObject.DORotate(localRotation, duration).SetEase(ease);
        }


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

            floatingObject.DOMove(startingPosition, duration).SetEase(ease);
            floatingObject.DORotate(startingRotation, duration).SetEase(ease);
            floatingObject.DOScale(startingScale, duration).SetEase(ease);
        }


    }
}

