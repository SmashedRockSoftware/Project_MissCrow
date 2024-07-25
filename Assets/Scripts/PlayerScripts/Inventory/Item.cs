using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public ItemScriptableObject itemData;
    [HideInInspector] public Transform locationOverride;

    [HideInInspector] public InspectAction inspectAction;
    [HideInInspector] public TalkAction talkAction;
    [HideInInspector] public PickupAction pickupAction;
    [HideInInspector] public UseAction useAction;

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

    }

    public void ChangeItemScriptableObject(ItemScriptableObject item) {
        itemData = item;
    }
}
