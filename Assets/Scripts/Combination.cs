using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combo", menuName = "ScriptableObjects/Combination", order = 1)]
public class Combination : ScriptableObject
{
    public Item item1;
    public Item item2;

    public Item combinedItem;
}
