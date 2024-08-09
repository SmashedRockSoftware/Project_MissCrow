using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class Car6 : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] Item item;
    [SerializeField] GameObject[] objectsToMoveToLayer;
    [SerializeField] DialogueScript[] dialogueScript;

    [Space]
    [Tooltip("Only objects with this tag will trigger this trigger, leave blank to allow anything to trigger this trigger")]
    [SerializeField] private protected string requiredTag = "Player";  //The tag we want to see to trigger

    [Tooltip("Unity event, which will execute when the required tag enters the collider")]
    [SerializeField] private protected UnityEvent OnEnter;

    [Tooltip("Delays the event firing after entering for X seconds")]
    [SerializeField] private float delayEnter;
    [Tooltip("Delays the event firing after exiting for X seconds")]
    [SerializeField] private float delayExit;

    bool firstEnter = true;
    int currentIndex;

    [SerializeField] AnimateObject mrCrowAnimateObject;
    const string MRCROW = "Mr. Crow";
    const string MRCROWTALK = "Armature_001|Mrcrowtalk-100";
    //const string MRCROWTEA = "Armature_001|Mrcrowtea-100";

    [SerializeField] AnimateObject scyllithAnimateObject;
    [SerializeField] AnimateObject teleportedScyllithAnimateObject;
    const string SCYLTURN = "ArmatureScyllith|Scyllithturn-100";
    const string SCYLRUN = "ArmatureScyllith|Scyllithrun-148";
    const string SCYLTEL = "ArmatureScyllith|ScyllithTeleported-50";
    const string END1 = "Mrs. Crow|Miss Scyllith, would you care for a bit of tea while you talk?";
    const string END2 = "Scyllith|I am the mighty Scyllith, princess of the great Kharitbr House. I will not be condescended to!";

    [SerializeField] PlayableDirector playableGivenTeaDirector;

    //[SerializeField] AnimateObject teaFogObject;
    //const string TEAFOG = "ArmatureTeafog1|Teafogmake-480";

    [SerializeField] ItemScriptableObject spiderTea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GivenSpiderTea() {
        Inventory.Instance.RemoveItem(spiderTea);
        //mrCrowAnimateObject.PlayAnimaiton(MRCROWTEA);
        //teaFogObject.PlayAnimaiton(TEAFOG);
        playableGivenTeaDirector.Play();
    }

    public void FinishedGivenCutscene() {
        GameManager.Instance.EnterCutsceneMode(dialogueScript[1].scriptList);
        GameManager.OnExitDialogue += OnFinishedDialogueTeleport;
    }

    public void OnFinishedDialogueTeleport() {
        scyllithAnimateObject.gameObject.SetActive(false);
        teleportedScyllithAnimateObject.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other) {
        if (!this.enabled) return;

        if (requiredTag == "") {
            Invoke(nameof(TriggerEntered), delayEnter);
        }
        else if (other.CompareTag(requiredTag)) {
            Invoke(nameof(TriggerEntered), delayEnter);
        }
    }

    private void OnEnable() {
        //GameManager.OnExitDialogue += OnExit;
        DialogueUI.OnNextDialogue += OnNextDialogue;
    }

    private void OnDisable() {
        GameManager.OnExitDialogue -= OnExit;
        DialogueUI.OnNextDialogue -= OnNextDialogue;
    }

    public void OnNextDialogue() {
        if (DialogueUI.Instance.CurrentDialogLine == END1) {
            scyllithAnimateObject.PlayAnimaiton(SCYLTURN);
        }

        if (DialogueUI.Instance.CurrentDialogLine == END2) {
            scyllithAnimateObject.PlayAnimaiton(SCYLRUN);
        }

        if (DialogueUI.Instance.CurrentDialogLine.Contains(MRCROW)) {
            mrCrowAnimateObject.PlayAnimaiton(MRCROWTALK);
        }

        //if(DialogueUI.Instance.CurrentDialogLine == "Mrs. Crow|Let�s at least get that sword out of her. ") {
        //    //scyllithAnimateObject.PlayAnimaiton(SCYLTEL);
        //    scyllithAnimateObject.gameObject.SetActive(false);
        //    teleportedScyllithAnimateObject.gameObject.SetActive(true);
        //}
    }

    void OnExit() {
        //Debug.Log("ON EXIT");

        ////GameManager.OnExitDialogue -= OnExit;

        //if (currentIndex == 1) {
        //    Debug.Log("INDEX 1 GameManager.OnExitDialogue += OnExit;");
        //    GameManager.OnExitDialogue += OnExit;
        //    scyllithAnimateObject.PlayAnimaiton(SCYLTURN);
        //}

        //NextDialogue();
    }

    void TriggerEntered() {
        if (!firstEnter) return;

        firstEnter = false;
        OnEnter.Invoke();

        GameManager.OnExitDialogue += OnExit;
        NextDialogue();
    }

    private void NextDialogue() {
        GameManager.Instance.EnterCutsceneMode(dialogueScript[currentIndex].scriptList);
        currentIndex++;
    }
}
