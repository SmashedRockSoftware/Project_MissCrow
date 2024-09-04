using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstInspect : MonoBehaviour {
    [SerializeField] DialogueScript dialogueScript;
    [SerializeField] TextAsset dialogueScriptTextAsset;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    [SerializeField] GameObject[] objectsToMoveToLayer;

    public void PerformFirstInspection() {
        if(dialogueScriptTextAsset != null)
            GameManager.Instance.EnterFirstInspectMode(virtualCamera, objectsToMoveToLayer, dialogueScriptTextAsset);
        else 
            GameManager.Instance.EnterFirstInspectMode(virtualCamera, objectsToMoveToLayer, dialogueScript.scriptList);
    }
}

