using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    [Space]
    [SerializeField] GameObject grannyPanel;
    [SerializeField] TextMeshProUGUI grannyText;
    [SerializeField] float grannyDisplayTime = 5f;
    //[SerializeField] Transform player;
    [SerializeField] Transform dialoguePoint;
    [SerializeField] Vector3 grannyPanelOffset = Vector3.up;
    Camera cam;

    float lifeTime = 0;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        cam = Camera.main;
        dialoguePoint = GameObject.Find("DialogPoint").transform;  //TODO this is likely to break
        if (dialoguePoint == null) Debug.LogError("DialogueUI::Start() Could not find the DialogPoint gameobject by name.  Create an empty object and place it above the player or else dialogue wont work");
    }

    public bool IsGrannyPanelInUse() {
        return grannyPanel.activeInHierarchy;
    }

    public void DisplayGrannyText(string text) {
        grannyPanel.SetActive(true); 
        grannyText.text = text;

        lifeTime = grannyDisplayTime;
    }

    public void HideGrannyText() {
        grannyPanel.SetActive(false);
    }

    private void Update() {
        if(grannyPanel.activeInHierarchy) 
            grannyPanel.transform.position = cam.WorldToScreenPoint(dialoguePoint.position);

        if(lifeTime > 0) lifeTime -= Time.deltaTime;

        if(lifeTime <= 0) HideGrannyText();
    }
}
