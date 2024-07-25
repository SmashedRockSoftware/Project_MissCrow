using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BadCombo", menuName = "ScriptableObjects/BadCombination", order = 1)]
public class BadCombo : ScriptableObject {
    public List<ItemScriptableObject> requiredItems = new List<ItemScriptableObject>();
    [TextArea(4, 100)] public string dialogueForBadCombo = "That won't work!";
}

