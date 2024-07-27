using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class ItemScriptableObject : ScriptableObject {
    public Sprite itemSprite;
    public string itemName;
    [TextArea(4, 100)] public string inspectDialogue = "FIX_ME";
}
