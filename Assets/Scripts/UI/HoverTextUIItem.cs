using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTextUIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    Item item;

    public void OnPointerEnter(PointerEventData eventData) {
        HoverNameUI.instance.ShowHoverText(item.itemData.itemName);
    }

    public void OnPointerExit(PointerEventData eventData) {
        HoverNameUI.instance.HideHoverText();
    }

    // Start is called before the first frame update
    void Start()
    {
        item = GetComponent<Item>();
    }
}
