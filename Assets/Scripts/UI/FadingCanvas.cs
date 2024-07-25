using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadingCanvas : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float fadeOutDuration = 1f;
    public static FadingCanvas instance;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        FadeIn();
    }

    public void FadeIn() {
        fadeImage.DOFade(0f, fadeInDuration).OnComplete(() => fadeImage.gameObject.SetActive(false));
    }

    public void FadeOut() {
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1f, fadeOutDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
