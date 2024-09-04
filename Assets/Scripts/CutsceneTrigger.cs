using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneTrigger : MonoBehaviour {
    [Tooltip("Only objects with this tag will trigger this trigger, leave blank to allow anything to trigger this trigger")]
    [SerializeField] private protected string requiredTag = "Player";  //The tag we want to see to trigger
    [Tooltip("Unity event, which will execute when the required tag enters the collider")]
    [SerializeField] private protected UnityEvent OnEnter;
    [SerializeField] private protected UnityEvent OnExitDialogue;

    [Tooltip("Delays the event firing after entering for X seconds")]
    [SerializeField] private float delayEnter;
    [Tooltip("Delays the event firing after exiting for X seconds")]
    [SerializeField] private float delayExit;

    [Space]
    [SerializeField] DialogueScript dialogueScript;
    [SerializeField] TextAsset dialogueTextAsset;
    //public List<string> scriptList = new List<string>();
    public List<CinemachineVirtualCamera> cameraShotList = new List<CinemachineVirtualCamera>();

    public CinemachineVirtualCamera startingVirtualCamera;
    public Item item;
    public GameObject[] objectsToMoveToLayer;

    private void Start() {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (!this.enabled) return;

        if (requiredTag == "") {
            Invoke(nameof(CallEventEnter), delayEnter);
        }
        else if (other.CompareTag(requiredTag)) {
            Invoke(nameof(CallEventEnter), delayEnter);
        }
    }

    public void OnExit() {
        GameManager.OnExitDialogue -= OnExit;
        OnExitDialogue.Invoke();
    }

    private void OnDisable() {
        GameManager.OnExitDialogue -= OnExit;
    }

    void CallEventEnter() {
        OnEnter.Invoke();

        if(dialogueTextAsset != null) 
            GameManager.Instance.EnterCutsceneMode(dialogueTextAsset);
        else
            GameManager.Instance.EnterCutsceneMode(dialogueScript.scriptList);

        GameManager.OnExitDialogue += OnExit;
        //GameManager.Instance.EnterTalkingMode(startingVirtualCamera.transform, item, objectsToMoveToLayer, dialogueScript.scriptList);
    }
}


