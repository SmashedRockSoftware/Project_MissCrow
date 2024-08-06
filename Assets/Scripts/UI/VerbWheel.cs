using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class VerbWheel : MonoBehaviour
{
    [SerializeField] int currentIndex = 1;
    [SerializeField] List<verbWheelVector> verbWheelVectors = new List<verbWheelVector>();
    [SerializeField] List<verbWheelEntry> verbWheelPoints = new List<verbWheelEntry>();
    [SerializeField] Image wheel;
    [SerializeField] float duration = 0.5f;

    [SerializeField] List<Item> hoveringItems = new List<Item>();

    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI comboText;

    Camera cam;

    public static VerbWheel instance;

    [Space]
    [SerializeField] GameObject nonRotbgObject;
    [SerializeField] GameObject backgroundObject;
    Vector3 backgroundObjectSize;
    [SerializeField] GameObject textObject;
    Vector3 textObjectSize;
    [SerializeField] float openDuration = 0.25f;
    [SerializeField] float closeDuration = 0.1f;
    Tween tweenNonRotBg;
    Tween tweenBg;
    Tween tweenTx;

    [Space]
    Material comboMaterial;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        backgroundObjectSize = backgroundObject.transform.localScale;
        textObjectSize = textObject.transform.localScale;
        HideVerbWheel();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.InGame) { return; }

        switch (PlayerCursor.Instance.currentCursorState) {
            case CursorState.None: 
                break;
            case CursorState.Look:
                currentIndex = 0;
                break;
            case CursorState.Talk:
                currentIndex = 2;
                break;
            case CursorState.Take:
                currentIndex = 1;
                break;
            case CursorState.Use:
                currentIndex = 3;
                break;
        }
        

        wheel.transform.DOLocalRotate(verbWheelPoints[currentIndex].rotationOfPoint, duration);

        for (int i = 0; i < verbWheelPoints.Count; i++) {
            verbWheelEntry verbWheelEntry = verbWheelPoints[i];
            verbWheelEntry.ShouldUnderline(i == currentIndex);
            verbWheelEntry.ShouldHighlightColor(i == currentIndex);
            verbWheelEntry.UpdatePositon();
        }

        transform.position = Input.mousePosition;
    }

    public void HoverEnter(Item item) {
        if(GameManager.Instance.currentGameState != GameState.InGame) { return; }

        hoveringItems.Add(item);

        ShowVerbWheel();

        itemNameText.text = item.itemData.itemName;

        if (hoveringItems.Count == 1) {
            verbWheelPoints[0].text.gameObject.SetActive(item.inspectAction != null);
            verbWheelPoints[1].text.gameObject.SetActive(item.pickupAction != null);
            verbWheelPoints[2].text.gameObject.SetActive(item.talkAction != null);
            verbWheelPoints[3].text.gameObject.SetActive(item.useAction != null);

            int currentIndex = 0;
            for (int i = 0; i < verbWheelPoints.Count; i++) {
                if (verbWheelPoints[i].text.gameObject.activeInHierarchy) {
                    verbWheelPoints[i].point = verbWheelVectors[currentIndex].point;
                    verbWheelPoints[i].rotationOfPoint = verbWheelVectors[currentIndex].rotationOfPoint;
                    currentIndex++;
                }
            }

            for (int i = 0; i < verbWheelPoints.Count; i++) {
                if (verbWheelPoints[i].text.gameObject.activeInHierarchy) {
                    currentIndex = i;
                    break;
                }
            }
        }
        else if (hoveringItems.Count == 2) {
            textObject.gameObject.SetActive(false);
            hoveringItems[0].MarkIsCombining();
            hoveringItems[1].MarkIsCombining();
            backgroundObject.gameObject.SetActive(false);
            nonRotbgObject.SetActive(false);
        }
    }

    private void ShowVerbWheel() {
        tweenNonRotBg.Kill(false);
        tweenBg.Kill(false);
        tweenTx.Kill(false);

        nonRotbgObject.transform.localScale = Vector3.zero;
        tweenNonRotBg = nonRotbgObject.transform.DOScale(backgroundObjectSize, openDuration);
        nonRotbgObject.SetActive(true);

        backgroundObject.transform.localScale = Vector3.zero;
        tweenBg = backgroundObject.transform.DOScale(backgroundObjectSize, openDuration);
        backgroundObject.SetActive(true);

        textObject.transform.localScale = Vector3.zero;
        tweenTx = textObject.transform.DOScale(textObjectSize, openDuration);
        textObject.SetActive(true);
    }

    public void HoverExit(Item item) {
        hoveringItems.Remove(item);

        if(hoveringItems.Count == 0) {
            HideVerbWheel();
        }
    }

    private void HideVerbWheel() {
        if (comboText == null) return;
        if (textObject == null) return;
        if (backgroundObject == null) return;

        textObject.gameObject.SetActive(false);
        verbWheelPoints[0].text.gameObject.SetActive(false);
        verbWheelPoints[1].text.gameObject.SetActive(false);
        verbWheelPoints[2].text.gameObject.SetActive(false);
        verbWheelPoints[3].text.gameObject.SetActive(false);

        comboText.gameObject.SetActive(false);

        if (tweenNonRotBg != null) tweenNonRotBg.Kill(false);
        if (tweenBg != null) tweenBg.Kill(false);
        if (tweenTx != null) tweenTx.Kill(false);

        tweenNonRotBg = nonRotbgObject.transform.DOScale(Vector3.zero, closeDuration).OnComplete(() => { nonRotbgObject.SetActive(false); });
        tweenBg = backgroundObject.transform.DOScale(Vector3.zero, closeDuration).OnComplete(() => { backgroundObject.SetActive(false); });
        tweenTx = textObject.transform.DOScale(Vector3.zero, closeDuration).OnComplete(() => { textObject.SetActive(false); });
    }
}

[System.Serializable]
public class verbWheelVector {
    public Image point;
    public Vector3 rotationOfPoint;
}

[System.Serializable]
public class verbWheelEntry {
    public Vector3 rotationOfPoint;
    public TextMeshProUGUI text;
    public Image point;

    const string underlineStart = "<u>";
    const string underlineEnd = "</u>";

    public Color highlight = Color.white;
    public Color regular = Color.gray;

    bool isUnderlinedd = false;

    public void UpdatePositon() {
        text.transform.position = point.transform.position;
    }

    public void ShouldUnderline(bool _isUnderlined) {
        if (isUnderlinedd && _isUnderlined) { return; }
        isUnderlinedd = _isUnderlined;

        string currentText = text.text;

        if (_isUnderlined) {
            text.text = underlineStart + currentText + underlineEnd;
        } else {
            currentText = currentText.Replace(underlineStart, "");
            currentText = currentText.Replace(underlineEnd, "");
            text.text = currentText;
        }
    }

    public void ShouldHighlightColor(bool _isHighlighted) {
        if (_isHighlighted) {
            text.color = highlight;
        }
        else {
            text.color = regular;
        }
    }
}
