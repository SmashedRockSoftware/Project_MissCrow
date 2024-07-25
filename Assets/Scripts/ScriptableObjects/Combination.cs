using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combo", menuName = "ScriptableObjects/Combination", order = 1)]
public class Combination : ScriptableObject
{
    public List<ItemScriptableObject> requiredItems = new List<ItemScriptableObject>();
    public List<ItemScriptableObject> cleanUpAfterCombo = new List<ItemScriptableObject>();

    public ItemScriptableObject combinedItem;
}
