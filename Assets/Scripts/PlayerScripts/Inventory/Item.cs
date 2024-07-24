using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public ItemScriptableObject itemData;
    [HideInInspector] public Transform locationOverride;

    //[Header("Inspect item settings")]
    //[SerializeField] UnityEvent onInspect;

    //[Header("Pickup item settings")]
    //[SerializeField] float pickupRadius = 1.25f;
    //float forcePickupAfter = 5f;
    //Transform player;
    //bool pickUpWhenNear;
    //[SerializeField] UnityEvent onPickup;

    //[Header("Talking item settings")]
    //[SerializeField] DialogueScript dialogueScript;
    //[SerializeField] CinemachineVirtualCamera virtualCamera;
    //[SerializeField] UnityEvent onTalk;

    public InspectAction inspectAction;
    public TalkAction talkAction;
    public PickupAction pickupAction;
    public UseAction useAction;

    // Start is called before the first frame update
    void Start()
    {
        locationOverride = transform.Find("locationOverride");

        inspectAction = gameObject.GetComponent<InspectAction>();
        talkAction = gameObject.GetComponent<TalkAction>();
        pickupAction = gameObject.GetComponent<PickupAction>();
        useAction = gameObject.GetComponent<UseAction>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (pickUpWhenNear && Vector3.Distance(player.position, transform.position) < pickupRadius) 
        //    PickInWorldItem();

    }

    //public void Inspect() {
    //    DialogueUI.Instance.DisplayGrannyText(itemData.inspectDialogue);
    //    onInspect.Invoke();
    //}

    #region Pickup
    //private void PickInWorldItem() {
    //    gameObject.SetActive(false);  //TODO this should happen when granny gets there or another things is pickedup
    //}



    //public void PickUp() {
    //    if (pickUpWhenNear) return;

    //    player = FindObjectOfType<PlayerMovement>().transform;

    //    Inventory.Instance.AddItem(itemData);

    //    pickUpWhenNear = true;
    //    Invoke(nameof(PickInWorldItem), forcePickupAfter);

    //    onPickup.Invoke();
    //}
    #endregion

    #region Talking
    //public void TalkToObject() {
    //    GameManager.Instance.EnterTalkingMode(virtualCamera.transform, this, dialogueScript.scriptList);
    //    onTalk.Invoke();
    //}
    #endregion

    //private void OnDrawGizmosSelected() {
    //    if(itemData.takeable)
    //        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    //}
}
