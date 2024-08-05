using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private const float MaxDistance = 200f;
    [SerializeField] List<DropTargets> dropTargets = new List<DropTargets>();
    List<GameObject> inventory = new List<GameObject>();
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform itemParent;
    [SerializeField] float minDistance = 1f;
    [SerializeField] float minDropTargetDistance = 70f;
    Camera cam;
    [SerializeField] private LayerMask layerMask1;

    [SerializeField] GameObject inventoryGameObject;

    // Start is called before the first frame update
    void Start()
    {
        Inventory.Instance.items.CollectionChanged += RebuildUI;
        dropTargets = FindObjectsByType<DropTargets>(FindObjectsSortMode.InstanceID).ToList();
        cam = Camera.main;
    }

    public void RemoveDropTarget(DropTargets dropTarget) {
        dropTargets.Remove(dropTarget);
    }

    public void AddDropTarget(DropTargets dropTarget) {
        dropTargets.Add(dropTarget);
    }

    void RebuildUI(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
        foreach (var item in inventory) {
            Destroy(item);
        }

        inventory.Clear();

        foreach (var item in Inventory.Instance.items) {
            if (item.itemSprite == null) continue;
            
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

        bool droppedItem = false;
        if (closestItem == null) {
            Vector3 itemScreenPos = cam.WorldToScreenPoint(Input.mousePosition);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 200f, Color.red, 5f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, MaxDistance, layerMask1)) {
                if (hit.collider.TryGetComponent<DropTargets>(out DropTargets dropTarget)) {
                    closestItem = dropTarget.gameObject;
                    droppedItem = true;
                }
            }
        }

        if (closestItem != null && droppedItem)
            PlayerMovement.instance.GoTo(closestItem.GetComponent<Item>());

        if (closestItem != null)
            Inventory.Instance.CombineItems(objectDropped, closestItem);

        if (closestItem == null)
            Debug.LogError("DropItem() No drop target for " + objectDropped.name);
    }

    // Update is called once per frame
    void Update()
    {
        inventoryGameObject.SetActive(GameManager.Instance.currentGameState != GameState.InDialogue);   
    }
}
