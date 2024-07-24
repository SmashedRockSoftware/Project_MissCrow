using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemScriptableObject itemData;
    public Transform locationOverride;

    [Header("Pickup item settings")]
    [SerializeField] float pickupRadius = 1.25f;
    float forcePickupAfter = 5f;
    Transform player;
    bool pickUpWhenNear;

    [Header("Talking item settings")]
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pickUpWhenNear && Vector3.Distance(player.position, transform.position) < pickupRadius) 
            PickInWorldItem();

    }

    #region Pickup
    private void PickInWorldItem() {
        gameObject.SetActive(false);  //TODO this should happen when granny gets there or another things is pickedup
    }

    public void PickUp() {
        if (pickUpWhenNear) return;

        player = FindObjectOfType<PlayerMovement>().transform;

        Inventory.Instance.AddItem(itemData);

        pickUpWhenNear = true;
        Invoke(nameof(PickInWorldItem), forcePickupAfter);
    }
    #endregion

    #region Talking
    public void TalkToObject() {
        CameraManager.instance.SetCameraToVisible(virtualCamera);
    }
    #endregion

    private void OnDrawGizmosSelected() {
        if(itemData.takeable)
            Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
