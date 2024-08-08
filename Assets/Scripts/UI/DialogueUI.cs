using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    int originalLayerMask;
    Item dialogueItem;
    bool inDialogue;

    [Header("Long Form Dialogue")]
    [SerializeField] List<string> dialogueText = new List<string>();
    [SerializeField] GameObject dialogueTextPanel;
    [SerializeField] TextMeshProUGUI dialogueButton;
    [SerializeField] TextMeshProUGUI textMeshDialogue;
    [SerializeField] TextMeshProUGUI nameTextMeshDialogue;
    int dialogueIndex;

    float lifeTime = 0;

    GameObject[] layerdSwappedObjs;

    GrannySounds grannySounds;

    [SerializeField] List<ChatterSounds> chatterSounds = new List<ChatterSounds>();

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        cam = Camera.main;
        dialoguePoint = GameObject.Find("dialoguePoint").transform;  //TODO this is likely to break
        if (dialoguePoint == null) Debug.LogError("DialogueUI::Start() Could not find the DialogPoint gameobject by name.  Create an empty object and place it above the player or else dialogue wont work");
        grannySounds = FindObjectOfType<GrannySounds>();
    }

    void RecursivelyChangeLayer(GameObject obj, int layerMask) {
        if (obj == null) return;
        // Set the layer of the current object
        obj.layer = layerMask;

        // Recursively traverse the children
        foreach (Transform child in obj.transform) {
            // Call this function for each child
            RecursivelyChangeLayer(child.gameObject, layerMask);
        }
    }

    public void EnterCutscene(List<string> dialogueScript) {
        //dialogueItem = item;

        //originalLayerMask = item.gameObject.layer;
        //var mask = LayerMask.NameToLayer("DialogueLayer");
        //RecursivelyChangeLayer(item.gameObject, mask);
        //foreach (var obj in objsToMove) {
        //    if (obj == null) continue;

        //    obj.layer = mask;
        //}

        //layerdSwappedObjs = objsToMove;

        //dialoguePanel.SetActive(true);
        //dialogueCamera.transform.position = camera.transform.position;
        //dialogueCamera.transform.rotation = camera.transform.rotation;
        //dialogueCamera.gameObject.SetActive(true);

        dialogueTextPanel.SetActive(true);
        dialogueText = dialogueScript;

        dialogueIndex = 0;
        DisplayNextDialogue();
    }

    public void EnterFirstInspect(CinemachineVirtualCamera camera, GameObject[] objsToMove, List<string> dialogueScript) {
        //dialogueItem = item;

        //originalLayerMask = item.gameObject.layer;
        var mask = LayerMask.NameToLayer("DialogueLayer");
        //RecursivelyChangeLayer(item.gameObject, mask);
        foreach (var obj in objsToMove) {
            if (obj == null) continue;

            obj.layer = mask;
        }

        layerdSwappedObjs = objsToMove;

        //dialoguePanel.SetActive(true);
        dialogueCamera.transform.position = camera.transform.position;
        dialogueCamera.transform.rotation = camera.transform.rotation;
        dialogueCamera.gameObject.SetActive(true);

        CameraManager.instance.SetCameraToVisible(camera, true);

        dialogueTextPanel.SetActive(true);
        dialogueText = dialogueScript;

        dialogueIndex = 0;
        DisplayNextDialogue();
    }

    public void EnterDialogue(Transform camera, Item item, GameObject[] objsToMove, List<string> dialogueScript) {
        dialogueItem = item;

        originalLayerMask = item.gameObject.layer;
        var mask = LayerMask.NameToLayer("DialogueLayer");
        RecursivelyChangeLayer(item.gameObject, mask);
        foreach (var obj in objsToMove) {
            if(obj == null) continue;

            obj.layer = mask;
        }

        layerdSwappedObjs = objsToMove;

        dialoguePanel.SetActive(true);
        dialogueCamera.transform.position = camera.transform.position;
        dialogueCamera.transform.rotation = camera.transform.rotation;
        dialogueCamera.gameObject.SetActive(true);

        dialogueTextPanel.SetActive(true);
        dialogueText = dialogueScript;

        dialogueIndex = 0;
        DisplayNextDialogue();
    }

    public void ExitDialogue() {
        if(dialogueItem != null)
            RecursivelyChangeLayer(dialogueItem.gameObject, originalLayerMask);

        CameraManager.instance.ReleaseForcedCamera();

        if (layerdSwappedObjs != null) {
            foreach (var obj in layerdSwappedObjs) {
                if (obj == null) continue;
                obj.layer = originalLayerMask;
            }
        }

        layerdSwappedObjs = null;

        GameManager.Instance.ExitTalkingMode();

        dialoguePanel.SetActive(false);
        dialogueCamera.gameObject.SetActive(false);
        dialogueItem = null;

        dialogueTextPanel.SetActive(false);
        dialogueText = null;

        nameTextMeshDialogue.text = "";
        textMeshDialogue.text = "";
    }

    public void DisplayNextDialogue() {
        if (dialogueIndex == dialogueText.Count) {
            ExitDialogue();
            return;
        }

        grannySounds?.InpectingAnItem();

        string nextLine = dialogueText[dialogueIndex];
        string[] splitLine = nextLine.Split('|');

        var chatterName = "";

        if (splitLine.Length > 1) {
            chatterName = splitLine[0];
            nameTextMeshDialogue.text = splitLine[0];
            textMeshDialogue.text = splitLine[1];
        }
        else {
            nameTextMeshDialogue.text = "";
            textMeshDialogue.text = splitLine[0];
        }

        if (dialogueIndex >= dialogueText.Count - 1) {
            dialogueButton.text = "End";
        }
        else {
            dialogueButton.text = "Next";
        }

        PlayChatterSounds(chatterName);

        dialogueIndex++;

        void PlayChatterSounds(string chatterName) {
            if (chatterName != "") {
                foreach (var chatter in chatterSounds) {
                    if (chatterName.ToLower().Contains(chatter.name.ToLower())) {
                        chatter.PlayChatter(cam.transform.position);
                    }
                }
            }
        }
    }

    public bool IsGrannyPanelInUse() {
        return grannyPanel.activeInHierarchy;
    }

    public void DisplayGrannyText(string text) {
        grannyPanel.SetActive(true); 
        grannyText.text = text;

        grannySounds?.InpectingAnItem();

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

        if(dialoguePanel.activeInHierarchy && Input.GetKeyUp(KeyCode.Tab)) {
            ExitDialogue();
        }

        if(dialogueItem != null && Input.GetKeyDown(KeyCode.Space)) {
            DisplayNextDialogue();
        }
    }
}

[System.Serializable]
public class ChatterSounds {
    public string name = "";
    [SerializeField] AudioClip[] chattersSFX;
    [SerializeField] AudioSource audioSource;

    [SerializeField] float minPitch = 0.8f, maxPitch = 1.2f, startingPitch = 1f;

    public void PlayChatter(Vector3 postion) {
        audioSource.transform.position = postion;

        if (chattersSFX.Length > 0) { 
            audioSource.clip = chattersSFX[Random.Range(0, chattersSFX.Length - 1)];
        }

        audioSource.pitch = startingPitch + Random.Range(minPitch, maxPitch);
        audioSource.Play();
    }
}
