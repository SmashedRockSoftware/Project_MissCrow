using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TalkAction : MonoBehaviour
{
    [SerializeField] DialogueScript dialogueScript;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] UnityEvent onTalk;

    [SerializeField] GameObject[] objectsToMoveToLayer;

    [SerializeField] bool isTalkAble = true;

    public void SetTalkability(bool shouldBeTalkable) {
        isTalkAble = shouldBeTalkable;
    }

    public void RemoveTalkingAbility() {
        Destroy(this);
    }

    public void SetDialogueScript(DialogueScript _dialogueScript) {
        dialogueScript = _dialogueScript;
    }

    public void TalkToObject() {
        if(!isTalkAble) { return; }

        GameManager.Instance.EnterTalkingMode(virtualCamera.transform, gameObject.GetComponent<Item>(), objectsToMoveToLayer, dialogueScript.scriptList);
        onTalk.Invoke();
    }
}
