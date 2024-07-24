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

    [Header("Zoom in settings")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] Camera dialogueCamera;
    [SerializeField] LayerMask layerMask;


    float lifeTime = 0;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        cam = Camera.main;
        dialoguePoint = GameObject.Find("DialogPoint").transform;  //TODO this is likely to break
        if (dialoguePoint == null) Debug.LogError("DialogueUI::Start() Could not find the DialogPoint gameobject by name.  Create an empty object and place it above the player or else dialogue wont work");
    }

    void RecursivelyChangeLayer(GameObject obj, LayerMask layerMask) {
        int dialogueLayer = LayerMask.NameToLayer("DialogueLayer");
        // Set the layer of the current object
        obj.layer = dialogueLayer;

        // Recursively traverse the children
        foreach (Transform child in obj.transform) {
            // Call this function for each child
            RecursivelyChangeLayer(child.gameObject, layerMask);
        }
    }

    public void EnterDialogue(Transform camera, Item item) {
        RecursivelyChangeLayer(item.gameObject, layerMask);

        dialoguePanel.SetActive(true);
        dialogueCamera.transform.position = camera.transform.position;
        dialogueCamera.transform.rotation = camera.transform.rotation;
        dialogueCamera.gameObject.SetActive(true);
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
