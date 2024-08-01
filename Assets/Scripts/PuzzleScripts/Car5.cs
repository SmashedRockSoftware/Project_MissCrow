using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Car5 : MonoBehaviour
{
    [SerializeField] Animator animator;

    //[SerializeField] bool tinUnCurledHasPlayed;

    //[Space]
    //[SerializeField] bool dialogue1;
    [SerializeField] DialogueScript dialogueScript1;
    [SerializeField] ItemScriptableObject itemScriptableObject1;

    //[Space]
    //[SerializeField] bool dialogue2;
    [SerializeField] DialogueScript dialogueScript2;
    [SerializeField] ItemScriptableObject itemScriptableObject2;

    //[Space]
    //[SerializeField] bool dialogue3;
    [SerializeField] DialogueScript dialogueScript3;
    [SerializeField] ItemScriptableObject itemScriptableObject3;

    //[Space]
    //[SerializeField] bool dialogue4;
    [SerializeField] DialogueScript dialogueScript4;
    [SerializeField] ItemScriptableObject itemScriptableObject4;

    [SerializeField] DialogueScript dialogueGive;

    //[Space]
    //[SerializeField] bool give;

    //[SerializeField] bool givenTheRequestedItem = true;
    [SerializeField] ItemScriptableObject requestedItem;
    [SerializeField] GameObject[] objectsToMoveToLayer;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Item item;

    [SerializeField] UnityEvent lastDialogue;

    //[SerializeField] TalkAction talkAction;

    const string idle = "ArmatureChefTin|ChefTinidleup";
    const string dialogue = "ArmatureChefTin|ChefTinidletalk-250";
    const string giveItem = "ArmatureChefTin|ChefTingive-100";

    [SerializeField] bool entryUpdate = true;

    //TinState tinState = TinState.dialogue1;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //public void GivenItem(ItemScriptableObject itemScriptableObject) {
    //    if(itemScriptableObject == requestedItem) {
    //        givenTheRequestedItem = true;
    //    }
    //}

    //public void SetupNextTalk() {

    //}

    private void Update() {
        if (!entryUpdate)
        {
            return;
        }

        if (GameManager.Instance.currentGameState != GameState.InGame) { return; }
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(idle)) {
            GameManager.Instance.EnterTalkingMode(virtualCamera.transform, item, objectsToMoveToLayer, dialogueScript1.scriptList);
            requestedItem = itemScriptableObject1;
            //enabled = false;
            entryUpdate = false;
        }
    }

    public void DroppedOn(GameObject objectDropped) {
        var dropped = objectDropped.GetComponent<Item>().itemData;

        if (dropped != requestedItem)
            return;

        if(dropped == itemScriptableObject1) {
            GameManager.Instance.EnterTalkingMode(virtualCamera.transform, item, objectsToMoveToLayer, dialogueScript2.scriptList);
            requestedItem = itemScriptableObject2;
        }

        if (dropped == itemScriptableObject2) {
            GameManager.Instance.EnterTalkingMode(virtualCamera.transform, item, objectsToMoveToLayer, dialogueScript3.scriptList);
            requestedItem = itemScriptableObject3;
        }

        if (dropped == itemScriptableObject3) {
            GameManager.Instance.EnterTalkingMode(virtualCamera.transform, item, objectsToMoveToLayer, dialogueScript4.scriptList);
            requestedItem = itemScriptableObject4;
        }

        if (dropped == itemScriptableObject4) {
            GameManager.Instance.EnterTalkingMode(virtualCamera.transform, item, objectsToMoveToLayer, dialogueGive.scriptList);
            lastDialogue.Invoke();
        }

    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if(GameManager.Instance.currentGameState != GameState.InGame) { return; }

    //    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
    //    if (stateInfo.IsName(idle) && givenTheRequestedItem) {
    //        //if (tinState == TinState.uncurling) {
    //        //    tinUnCurledHasPlayed = true;
    //        //    animator.Play(idle);
    //        //    givenTheRequestedItem = false;
    //        //} 
    //        //else 
    //        if (tinState == TinState.dialogue1) {
    //            dialogue1 = true;
    //            animator.Play(dialogue);
    //            requestedItem = itemScriptableObject1;
    //            givenTheRequestedItem = false;
    //            GameManager.Instance.EnterTalkingMode(virtualCamera.transform, item, objectsToMoveToLayer, dialogueScript1.scriptList);
    //            tinState = TinState.dialogue2;
    //            return;
    //        }

    //        if (tinState == TinState.dialogue2) {
    //            dialogue2 = true;
    //            animator.Play(dialogue);
    //            requestedItem = itemScriptableObject2;
    //            givenTheRequestedItem = false;
    //            GameManager.Instance.EnterTalkingMode(virtualCamera.transform, item, objectsToMoveToLayer, dialogueScript2.scriptList);
    //            tinState = TinState.dialogue3;
    //            return;
    //        }

    //        if (tinState == TinState.dialogue3) {
    //            dialogue3 = true;
    //            animator.Play(dialogue);
    //            requestedItem = itemScriptableObject3;
    //            givenTheRequestedItem = false;
    //            GameManager.Instance.EnterTalkingMode(virtualCamera.transform, item, objectsToMoveToLayer, dialogueScript3.scriptList);
    //            tinState = TinState.dialogue4;
    //            return;
    //        }

    //        if (tinState == TinState.dialogue4) {
    //            dialogue4 = true;
    //            animator.Play(dialogue);
    //            requestedItem = itemScriptableObject4;
    //            givenTheRequestedItem = false;
    //            GameManager.Instance.EnterTalkingMode(virtualCamera.transform, item, objectsToMoveToLayer, dialogueScript4.scriptList);
    //            tinState = TinState.give;
    //            return;
    //        }

    //        if (tinState == TinState.give) {
    //            give = true;
    //            animator.Play(giveItem);
    //        }
    //    }
    //}
}

public enum TinState {
    uncurling,
    dialogue1,
    dialogue2,
    dialogue3,
    dialogue4,
    give
}
