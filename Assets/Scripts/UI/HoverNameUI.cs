using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoverNameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    public static HoverNameUI instance;
    Camera cam;

    [SerializeField] Vector3 offset = Vector3.up * 10;
    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        HideHoverText();
    }

    // Update is called once per frame
    void Update()
    {
        if (textMeshProUGUI.gameObject.activeInHierarchy) {
            textMeshProUGUI.transform.position = Input.mousePosition + offset;
        }
    }

    public void ShowHoverText(string text) {
        textMeshProUGUI.gameObject.SetActive(true);
        textMeshProUGUI.text = text;
    }

    public void HideHoverText() {
        textMeshProUGUI.gameObject.SetActive(false);    
    }
}
