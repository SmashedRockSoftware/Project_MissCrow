using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ObservableCollection<ItemScriptableObject> items = new ObservableCollection<ItemScriptableObject>();
    public static Inventory Instance;
    [SerializeField] List<Combination> combinations = new List<Combination>();

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemScriptableObject item) {
        items.Add(item);
    }

    public Combination CheckCombinations(List<ItemScriptableObject> currentItems) {
        Combination result = combinations.FirstOrDefault(c => new HashSet<ItemScriptableObject>(c.requiredItems).SetEquals(new HashSet<ItemScriptableObject>(currentItems)));

        if (result != null) {
            Debug.Log("Inventory::CheckCombinations() Matched combination: " + string.Join(", ", result.requiredItems));
            return result;
        }
        else {
            Debug.Log("Inventory::CheckCombinations()  No match found.");
            return null;
        }
    }

    public void CombineItems(GameObject objectDropped, GameObject closestItem) {
        Debug.Log("Combine these items " + objectDropped.name + " to " + closestItem.name);

        List<ItemScriptableObject> currentObjects = new List<ItemScriptableObject>();
        currentObjects.Add(objectDropped.GetComponent<Item>().itemData);
        currentObjects.Add(closestItem.GetComponent<Item>().itemData);

        var Combo = CheckCombinations(currentObjects);

        if(Combo != null) {
            foreach (var item in Combo.requiredItems) {
                items.Remove(item);
            }

            items.Add(Combo.combinedItem);
        }
    }
}
