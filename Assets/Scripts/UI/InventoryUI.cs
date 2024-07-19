using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    List<GameObject> inventory = new List<GameObject>();
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform itemParent;

    // Start is called before the first frame update
    void Start()
    {
        Inventory.Instance.items.CollectionChanged += RebuildUI;
    }

    void RebuildUI(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
        foreach (var item in inventory) {
            Destroy(item);
        }

        inventory.Clear();

        foreach (var item in Inventory.Instance.items) {
            var itemUi = Instantiate(itemPrefab) as GameObject;
            itemUi.transform.SetParent(itemParent, false);
            itemUi.GetComponent<Image>().sprite = item.sprite;
            //TODO add a ref to the item
            inventory.Add(itemUi);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
