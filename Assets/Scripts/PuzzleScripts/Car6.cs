using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car6 : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] Item item;
    [SerializeField] GameObject[] objectsToMoveToLayer;
    [SerializeField] DialogueScript dialogueScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterDialogue() {
        GameManager.Instance.EnterTalkingMode(virtualCamera.transform, item, objectsToMoveToLayer, dialogueScript.scriptList);
    }
}
