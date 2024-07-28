using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class VerbWheel : MonoBehaviour
{
    [SerializeField] int currentIndex = 1;
    [SerializeField] List<verbWheelEntry> verbWheelPoints = new List<verbWheelEntry>();
    [SerializeField] Image wheel;
    [SerializeField] float duration = 0.5f;

    [SerializeField] List<Item> hoveringItems = new List<Item>();

    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI comboText;

    Camera cam;

    public static VerbWheel instance;

    [Space]
    [SerializeField] GameObject backgroundObject;
    Vector3 backgroundObjectSize;
    [SerializeField] GameObject textObject;
    Vector3 textObjectSize;
    [SerializeField] float openDuration = 0.25f;
    [SerializeField] float closeDuration = 0.1f;
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
        int maxIndex = 3;

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f) {
            if (scrollInput > 0f) {
                int i = 0;
                do {
                    i++;
                    currentIndex++; 
                    if (currentIndex > maxIndex) { currentIndex = 0; }
                    if (i > verbWheelPoints.Count) break;
                } while (!verbWheelPoints[currentIndex].text.gameObject.activeInHierarchy);
            }
            else if (scrollInput < 0f) {
                int i = 0;
                do {
                    i++;
                    currentIndex--;
                    if (currentIndex < 0) { currentIndex = maxIndex; }
                    if (i > verbWheelPoints.Count) break;
                } while (!verbWheelPoints[currentIndex].text.gameObject.activeInHierarchy);
            }
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
        hoveringItems.Add(item);

        ShowVerbWheel();

        itemNameText.text = item.itemData.itemName;

        if (hoveringItems.Count == 1) {
            verbWheelPoints[0].text.gameObject.SetActive(item.inspectAction != null);
            verbWheelPoints[1].text.gameObject.SetActive(item.pickupAction != null);
            verbWheelPoints[2].text.gameObject.SetActive(item.talkAction != null);
            verbWheelPoints[3].text.gameObject.SetActive(item.useAction != null);

            for (int i = 0; i < verbWheelPoints.Count; i++) {
                if (verbWheelPoints[i].text.gameObject.activeInHierarchy) {
                    currentIndex = i;
                    break;
                }
            }

        }
        else if (hoveringItems.Count == 2) {
            
            ////TODO add combo
            //TODO grab render underneath and change the secondary material to a different color outline, then clean it up somehow
            //itemNameText.text = hoveringItems[0].itemData.itemName + " " + hoveringItems[1].itemData.itemName;

            //TODO grab render underneath and change the secondary material to a different color outline, then clean it up somehow
            textObject.gameObject.SetActive(false);
            hoveringItems[0].MarkIsCombining();
            hoveringItems[1].MarkIsCombining();
            //if (hoveringItems[0].TryGetComponent<Renderer>(out Renderer rend)) {
            //    rend.materials[1] = comboMaterial;
            //}
            //if (hoveringItems[1].TryGetComponent<Renderer>(out Renderer rendr)) {

            //}
            backgroundObject.gameObject.SetActive(false);
            //comboText.gameObject.SetActive(true);

            //comboText.text = "Combine <u>" + hoveringItems[0].itemData.itemName + "</u> with <u>" + hoveringItems[1].itemData.itemName + "</u>";
        }
    }

    private void ShowVerbWheel() {
        tweenBg.Kill(false);
        tweenTx.Kill(false);

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
        comboText.gameObject.SetActive(false);
        tweenBg.Kill(false);
        tweenTx.Kill(false);

        tweenBg = backgroundObject.transform.DOScale(Vector3.zero, closeDuration).OnComplete(() => { backgroundObject.SetActive(false); });
        tweenTx = textObject.transform.DOScale(Vector3.zero, closeDuration).OnComplete(() => { textObject.SetActive(false); });
    }
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
