using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BadCombo", menuName = "ScriptableObjects/BadCombination", order = 1)]
public class BadCombo : ScriptableObject {
    public List<ItemScriptableObject> requiredItems = new List<ItemScriptableObject>();
    [TextArea(4, 100)] public string dialogueForBadCombo = "That won't work!";

    public BadCombo(ItemScriptableObject item1, ItemScriptableObject item2, string dialogueForBadCombo) {
        this.requiredItems.Add(item1);
        this.requiredItems.Add(item2);
        this.dialogueForBadCombo = dialogueForBadCombo;
    }
}

