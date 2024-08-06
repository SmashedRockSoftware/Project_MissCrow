using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WormBottleRattle : MonoBehaviour
{
    [SerializeField] Transform wormVisuals;
    [SerializeField] private Vector3 endRotation;
    [SerializeField] private float duration = 0.25f;
    Tween tween;

    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        tween = wormVisuals.DORotate(endRotation, duration).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stop() {
        if (tween != null) {
            tween.Kill();
            wormVisuals.DORotate(Vector3.zero, duration);
        }

        if(audioSource != null) {
            audioSource.Stop();
        }
    }
}

