using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstInspect : MonoBehaviour {
    [SerializeField] DialogueScript dialogueScript;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    [SerializeField] GameObject[] objectsToMoveToLayer;

    public void PerformFirstInspection() {
        GameManager.Instance.EnterFirstInspectMode(virtualCamera, objectsToMoveToLayer, dialogueScript.scriptList);
    }
}

