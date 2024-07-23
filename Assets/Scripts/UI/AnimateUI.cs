using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class AnimateUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
    [SerializeField] Vector3 scaleSize = Vector3.one;
    [SerializeField] Vector3 pickedUpSize = Vector3.one;
    [SerializeField] float duration = 1f;
    [SerializeField] Ease ease = Ease.InOutCirc;
    Vector3 originalScale;

    [SerializeField] Vector3 punchScaleSize = Vector3.one;
    [SerializeField] float punchDuration = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData) {
        transform.DOScale(scaleSize, duration).SetEase(ease);
    }


    public void OnPointerDown(PointerEventData eventData) {
        transform.DOPunchScale(punchScaleSize, punchDuration);
        transform.DOScale(pickedUpSize, duration).SetEase(ease);
    }


    public void OnPointerExit(PointerEventData eventData) {
        transform.DOScale(originalScale, duration).SetEase(ease);
    }
}
