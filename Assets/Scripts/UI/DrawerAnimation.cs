using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DrawerAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] Vector3 startingPosition;
    [SerializeField] float start;
    [SerializeField] Vector3 hidePosition = Vector3.down * 100;
    float duration = 0.25f;

    Camera cam;

    Transform hideTransform, showTransform;

    [SerializeField] Ease ease = Ease.InOutCirc;

    public void OnPointerEnter(PointerEventData eventData) {
        //transform.position = cam.WorldToScreenPoint(showTransform.position);
        transform.DOMove(cam.WorldToScreenPoint(showTransform.position), duration).SetEase(ease);
    }

    public void OnPointerExit(PointerEventData eventData) {
        //transform.position = cam.WorldToScreenPoint(hideTransform.position);
        transform.DOMove(cam.WorldToScreenPoint(hideTransform.position), duration).SetEase(ease);
    }

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.localPosition;
        start = transform.position.y;

        hideTransform = GameObject.Find("hideTransform").transform;
        showTransform = GameObject.Find("showTransform").transform;

        if(hideTransform == null) { Debug.LogError("DrawerAnimation::Start() hideTransform is missing or renamed!"); }
        if (showTransform == null) { Debug.LogError("DrawerAnimation::Start() showTransform is missing or renamed!"); }

        cam = Camera.main;

        transform.position = cam.WorldToScreenPoint(hideTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
