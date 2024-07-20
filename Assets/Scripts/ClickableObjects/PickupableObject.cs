using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableObject : MonoBehaviour
{
    [SerializeField] Item item;

    bool pickUpWhenNear;
    [SerializeField] float pickupRadius = 1.25f;
    float forcePickupAfter = 5f;
    Transform player;


    private void Update() {
        if(pickUpWhenNear && Vector3.Distance(player.position, transform.position) < pickupRadius) {
            PickInWorldItem();
        }
    }

    private void PickInWorldItem() {
        gameObject.SetActive(false);  //TODO this should happen when granny gets there or another things is pickedup
    }

    void OnMouseOver() {
        if (GameManager.Instance.currentGameState != GameState.InGame) return;

        if (Input.GetMouseButtonDown(0)) {
            if (pickUpWhenNear) return;

            player = FindObjectOfType<PlayerMovement>().transform;

            Inventory.Instance.AddItem(item);

            pickUpWhenNear = true;
            Invoke(nameof(PickInWorldItem), forcePickupAfter);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
