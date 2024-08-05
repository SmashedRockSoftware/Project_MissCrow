using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Script", menuName = "ScriptableObjects/DialogueScript", order = 1)]
public class DialogueScript : ScriptableObject
{
    public List<string> scriptList = new List<string>();
    public List<CinemachineVirtualCamera> cameraShotList = new List<CinemachineVirtualCamera>();

    public CinemachineVirtualCamera startingVirtualCamera;
    public Item item;
    public GameObject[] objectsToMoveToLayer;
}
