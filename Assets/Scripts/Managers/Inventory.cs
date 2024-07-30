using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const string COMBOSOUNDNAME = "combination";
    private const string BADCOMBOSOUNDNAME = "badcombination";
    public ObservableCollection<ItemScriptableObject> items = new ObservableCollection<ItemScriptableObject>();
    public static Inventory Instance;
    [SerializeField] List<Combination> combinations = new List<Combination>();
    [SerializeField] List<EventOnCombo> comboEvents = new List<EventOnCombo>();
    [SerializeField] List<EventOnPickup> pickupEvents = new List<EventOnPickup>();
    [SerializeField] List<EventOnUse> useEvents = new List<EventOnUse>();

    [SerializeField] List<BadCombo> badCombos = new List<BadCombo>();

    [SerializeField] List<ItemScriptableObject> itemObjects = new List<ItemScriptableObject>();

    Camera cam;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        cam = Camera.main;
    }

    private void Start() {
        comboEvents.AddRange(FindObjectsOfType<EventOnCombo>().ToList());
        pickupEvents.AddRange(FindObjectsOfType<EventOnPickup>().ToList());
        useEvents.AddRange(FindObjectsOfType<EventOnUse>().ToList());

        ReadInBadCombosText();
    }

    private void ReadInBadCombosText() {
        var textFile = Resources.Load<TextAsset>("badcombos");
        string[] lines = textFile.text.Split('\n');

        foreach (string line in lines) {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var splitComboDialog = line.Split(':');
            if (splitComboDialog.Length != 2) continue;

            var splitCombo = splitComboDialog[0].Split('+');
            if (splitCombo.Length != 2) continue;

            ItemScriptableObject item1 = FindItem(splitCombo[0].Trim());
            ItemScriptableObject item2 = FindItem(splitCombo[1].Trim());

            if (item1 != null && item2 != null) {
                badCombos.Add(new BadCombo(item1, item2, splitComboDialog[1].Trim()));
            }
            else {
                Debug.LogWarning($"Couldn't find items for combination: {line}");
            }
        }

        ItemScriptableObject FindItem(string itemName) {
            return itemObjects.FirstOrDefault(item => item.name.ToLower() == itemName.ToLower());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemScriptableObject item) {
        items.Add(item);

        foreach (var picked in pickupEvents) {
            if(picked.requiredItem == item) {
                picked.OnPickupFireEvent();
            }
        }
    }

    public void ItemBeingUsed(ItemScriptableObject item) {
        foreach (var used in useEvents) {
            if (used.requiredItem == item) {
                used.OnUseFireEvent();
            }
        }
    }

    public Combination CheckCombinations(List<ItemScriptableObject> currentItems) {
        Combination result = combinations.FirstOrDefault(c => new HashSet<ItemScriptableObject>(c.requiredItems).SetEquals(new HashSet<ItemScriptableObject>(currentItems)));

        if (result != null) {
            Debug.Log("Inventory::CheckCombinations() Matched combination["+ result.name+ "]: " + string.Join(", ", result.requiredItems));
            return result;
        }
        else {
            Debug.Log("Inventory::CheckCombinations()  No match found. [" + currentItems[0] + ", " + currentItems[1] + "]");
            return null;
        }
    }

    public BadCombo CheckBadCombinations(List<ItemScriptableObject> currentItems) {
        BadCombo result = badCombos.FirstOrDefault(c => new HashSet<ItemScriptableObject>(c.requiredItems).SetEquals(new HashSet<ItemScriptableObject>(currentItems)));

        if (result != null) {
            Debug.Log("Inventory::CheckBadCombinations() Matched combination: " + string.Join(", ", result.requiredItems));
            return result;
        }
        else {
            Debug.Log("Inventory::CheckBadCombinations()  No match found. [" + currentItems[0] + ", " + currentItems[1] + "]");
            return null;
        }
    }

    public void CombineItems(GameObject objectDropped, GameObject closestItem) {
        Debug.Log("Inventory::CombineItems [" + objectDropped.name + " with " + closestItem.name + "]");

        List<ItemScriptableObject> currentObjects = new List<ItemScriptableObject>();
        currentObjects.Add(objectDropped.GetComponent<Item>().itemData);
        currentObjects.Add(closestItem.GetComponent<Item>().itemData);

        var Combo = CheckCombinations(currentObjects);

        if (Combo != null) {
            PerformCombo(Combo, closestItem.transform.position);
        }
        else {
            PerformBadCombos(currentObjects);
        }
    }

    private void PerformCombo(Combination Combo, Vector3 location) {
        FireRelatedEvents(Combo);

        AudioAssistant.instance.PlayResourceSoundAtPoint(Combo, COMBOSOUNDNAME, location);

        foreach (var item in Combo.requiredItems) {
            items.Remove(item);
        }

        if (Combo.combinedItem != null)
            items.Add(Combo.combinedItem);
    }

    private void PerformBadCombos(List<ItemScriptableObject> currentObjects) {
        var BadCombo = CheckBadCombinations(currentObjects);

        AudioAssistant.instance.PlayResourceSoundAtPoint(BadCombo, BADCOMBOSOUNDNAME, cam.transform.position);

        if (BadCombo != null)
            DialogueUI.Instance.DisplayGrannyText(BadCombo.dialogueForBadCombo);
        else {
            DialogueUI.Instance.DisplayGrannyText("That won't work");
        }
    }

    private void FireRelatedEvents(Combination Combo) {
        foreach (var comboEvent in comboEvents) {
            if (comboEvent.requiredCombo == Combo) {
                comboEvent.OnCombinationFireEvent();
            }
        }
    }
}
