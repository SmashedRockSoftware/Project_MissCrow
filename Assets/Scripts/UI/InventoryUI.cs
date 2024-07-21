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
    [SerializeField] float minDistance = 1f;

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
            itemUi.GetComponent<Image>().sprite = item.itemSprite;
            itemUi.name = item.name;
            itemUi.GetComponent<Item>().itemData = item;
            inventory.Add(itemUi);
        }
    }

    public void DropItem(GameObject objectDropped) {
        float distance = Mathf.Infinity;
        GameObject closestItem = null;

        foreach(var item in inventory) {
            if(item ==  objectDropped) { continue; }

            var dist = Vector3.Distance(item.transform.position, objectDropped.transform.position);

            if (dist < distance)
            {
                distance = dist;

                if(dist < minDistance)
                    closestItem = item.gameObject;
            }
        }

        if (closestItem != null)
            Inventory.Instance.CombineItems(objectDropped, closestItem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
