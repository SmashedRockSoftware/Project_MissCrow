using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTextUIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    Item item;

    public void OnPointerEnter(PointerEventData eventData) {
        //HoverNameUI.instance.ShowHoverText(item.itemData.itemName);
        //HoverNameUI.instance.HoverOverItem(item);
    }

    public void OnPointerExit(PointerEventData eventData) {
        //HoverNameUI.instance.HideHoverText();
        //HoverNameUI.instance.leaveOverItem(item);
    }

    void OnMouseEnter() {

    }

    void OnMouseExit() {

    }

    // Start is called before the first frame update
    void Start()
    {
        item = GetComponent<Item>();
    }
}
