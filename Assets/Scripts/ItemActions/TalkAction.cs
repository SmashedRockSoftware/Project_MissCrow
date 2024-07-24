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

    public void TalkToObject() {
        GameManager.Instance.EnterTalkingMode(virtualCamera.transform, gameObject.GetComponent<Item>(), dialogueScript.scriptList);
        onTalk.Invoke();
    }
}
