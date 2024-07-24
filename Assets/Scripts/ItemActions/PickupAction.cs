using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupAction : MonoBehaviour
{
    public ItemScriptableObject itemData;
    [HideInInspector] public Transform locationOverride;
    [SerializeField] float pickupRadius = 1.25f;
    [SerializeField] float forcePickupAfter = 5f;
    Transform player;
    bool pickUpWhenNear;
    [SerializeField] UnityEvent onPickup;

    // Start is called before the first frame update
    void Start() {
        locationOverride = transform.Find("locationOverride");
        itemData = gameObject.GetComponent<Item>().itemData;
    }

    // Update is called once per frame
    void Update() {
        if (pickUpWhenNear && Vector3.Distance(player.position, transform.position) < pickupRadius)
            PickInWorldItem();
    }

    private void PickInWorldItem() {
        gameObject.SetActive(false);
    }

    public void PickUp() {
        if (pickUpWhenNear) return;

        player = FindObjectOfType<PlayerMovement>().transform;

        Inventory.Instance.AddItem(itemData);

        pickUpWhenNear = true;
        Invoke(nameof(PickInWorldItem), forcePickupAfter);

        onPickup.Invoke();
    }


    private void OnDrawGizmosSelected() {
        //if (itemData.takeable)
            Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
