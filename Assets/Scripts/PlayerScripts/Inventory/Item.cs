using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public ItemScriptableObject itemData;
    [HideInInspector] public Transform locationOverride;

    [HideInInspector] public InspectAction inspectAction;
    [HideInInspector] public TalkAction talkAction;
    [HideInInspector] public PickupAction pickupAction;
    [HideInInspector] public UseAction useAction;

    ItemGlow glower;

    // Start is called before the first frame update
    void Start()
    {
        locationOverride = transform.Find("locationOverride");

        inspectAction = gameObject.GetComponent<InspectAction>();
        talkAction = gameObject.GetComponent<TalkAction>();
        pickupAction = gameObject.GetComponent<PickupAction>();
        useAction = gameObject.GetComponent<UseAction>();

        glower = gameObject.GetComponent<ItemGlow>();
    }

    public void MarkIsCombining() {
        if (glower == null) return;
        glower.MarkAsCombined();
    }

    public void MarkNormOutline() {
        if (glower == null) return;
        glower.MarkAsRegular();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //HoverNameUI.instance.ShowHoverText(item.itemData.itemName);
        VerbWheel.instance.HoverEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData) {
        //HoverNameUI.instance.HideHoverText();
        VerbWheel.instance.HoverExit(this);
        MarkNormOutline();

    }

    void OnMouseEnter() {
        VerbWheel.instance.HoverEnter(this);
    }

    void OnMouseExit() {
        VerbWheel.instance.HoverExit(this);
        MarkNormOutline();
    }

    private void OnDisable() {
        VerbWheel.instance.HoverExit(this);
        MarkNormOutline();
    }

    private void OnDestroy() {
        VerbWheel.instance.HoverExit(this);
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeItemScriptableObject(ItemScriptableObject item) {
        itemData = item;
    }
}
