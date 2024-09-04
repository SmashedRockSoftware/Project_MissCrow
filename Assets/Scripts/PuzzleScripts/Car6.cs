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
    [SerializeField] TextAsset ScylithCombo1;

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
    const string END1 = "Miss Scyllith, would you care for a bit of tea while you talk?";
    const string END2 = "I am the mighty Scyllith, princess of the great Kharitbr House. I will not be condescended to!";

    [SerializeField] PlayableDirector playableGivenTeaDirector;

    [SerializeField] AnimateObject teleportAnimatedObject;
    const string TELEUSE = "Teleport1|Teleportmagicuse";
    [SerializeField] GameObject telePortParticle;
    [SerializeField] GameObject telePortParticle1;

    [SerializeField] AnimateObject knightSwordRemoveAnimateObject;
    //Teleport1|Teleportmagicuse

    //[SerializeField] AnimateObject teaFogObject;
    //const string TEAFOG = "ArmatureTeafog1|Teafogmake-480";

    [SerializeField] ItemScriptableObject spiderTea;

    [Space]
    [SerializeField] GameObject knightSword;
    [SerializeField] AnimateObject animatedKnightSword;
    [SerializeField] GameObject crowBanishAnimatedObject;
    const string SWRDANIM = "ArmatureScyllith|KnightswordRemove";
    const string SCYFOUR = "ArmatureScyllith|ScyllithFourth-100";
    const string CRWMAGI = "Armature_001|Teleportmagicuse";
    const string CRWBANI = "ArmatureCrowbanish1|Crowbanishanimation";

    [SerializeField] float beforeTalkSwordTime = 3f;
    [SerializeField] int nextLevel = 2;
    //

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            OnFinishedDialogueTeleport();
        }
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

    public void OnSwordPickedup() {
        knightSword.gameObject.SetActive(false);
        animatedKnightSword.gameObject.SetActive(true);
        crowBanishAnimatedObject.gameObject.SetActive(true);
        animatedKnightSword.PlayAnimaiton(SWRDANIM);
        mrCrowAnimateObject.PlayAnimaiton(CRWMAGI);
        //crowBanishAnimatedObject.PlayAnimaiton(CRWBANI);
        StartCoroutine(SwordCoRoutine());

        //knightSwordRemoveAnimateObject.PlayAnimaiton("Knightsword|KnightswordRemove");
        //teleportAnimatedObject.PlayAnimaiton("ArmatureScyllith|ScyllithFourth-100");
    }

    IEnumerator SwordCoRoutine() {
        yield return new WaitForSeconds(beforeTalkSwordTime);
        teleportedScyllithAnimateObject.PlayAnimaiton(SCYFOUR);
        GameManager.Instance.EnterCutsceneMode(dialogueScript[2].scriptList);
        GameManager.OnExitDialogue += OnFinishedGame;

        //
    }

    public void OnFinishedGame() {
        GameManager.OnExitDialogue -= OnFinishedGame;
        gameObject.SendMessage("FadeThenLoadLevel", nextLevel);
    }

    public void OnFinishedDialogueTeleport() {
        scyllithAnimateObject.gameObject.SetActive(false);
        teleportedScyllithAnimateObject.gameObject.SetActive(true);

        telePortParticle.SetActive(true);
        telePortParticle1.SetActive(true);

        teleportAnimatedObject.PlayAnimaiton(TELEUSE);
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

    public void animatedEvent(string value) {
        if (value == "ScyllithTurn") {
            scyllithAnimateObject.PlayAnimaiton(SCYLTURN);
            return;
        }

        if (value == "ScyllithLeave") {
            scyllithAnimateObject.PlayAnimaiton(SCYLRUN);
            return;
        }
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

        //if(DialogueUI.Instance.CurrentDialogLine == "Mrs. Crow|Let’s at least get that sword out of her. ") {
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
        if(currentIndex == 0) {
            GameManager.Instance.EnterCutsceneMode(ScylithCombo1);
        } else
            GameManager.Instance.EnterCutsceneMode(dialogueScript[currentIndex].scriptList);

        currentIndex++;
    }
}
