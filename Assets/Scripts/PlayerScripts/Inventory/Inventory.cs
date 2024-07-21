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
    [SerializeField] List<EventOnCombo> comboEvents = new List<EventOnCombo>();

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
        Debug.Log("Combine [" + objectDropped.name + " with " + closestItem.name + "]");

        List<ItemScriptableObject> currentObjects = new List<ItemScriptableObject>();
        currentObjects.Add(objectDropped.GetComponent<Item>().itemData);
        currentObjects.Add(closestItem.GetComponent<Item>().itemData);

        var Combo = CheckCombinations(currentObjects);

        if (Combo != null) {
            FireRelatedEvents(Combo);

            foreach (var item in Combo.requiredItems) {
                items.Remove(item);
            }

            if (Combo.combinedItem != null)
                items.Add(Combo.combinedItem);
        }
    }

    private void FireRelatedEvents(Combination Combo) {
        foreach (var comboEvent in comboEvents) {
            if (comboEvent.requiredCombo == Combo) {
                comboEvent.OnCombinationFireEvent();
                break;
            }
        }
    }
}
