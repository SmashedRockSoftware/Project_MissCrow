using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class swingingAnimation : MonoBehaviour {
    [SerializeField] Transform swingingTransform;
    [SerializeField] private Vector3 endRotation;
    [SerializeField] private float duration = 0.25f;
    Tween tween;

    [SerializeField] Ease ease = Ease.InOutSine;

    //[SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        tween = swingingTransform.DORotate(endRotation, duration).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
    }

    // Update is called once per frame
    void Update() {

    }

    public void Stop() {
        if (tween != null) {
            tween.Kill();
            swingingTransform.DORotate(Vector3.zero, duration);
        }

        //if (audioSource != null) {
        //    audioSource.Stop();
        //}
    }
}

