using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    const float MAXRAYLENGTH = 100f;
    [SerializeField] private LayerMask layerMask = ~0;
    Item currentItem;

    public CursorState currentCursorState = CursorState.Look;
    public static event System.Action OnCursorChange;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    public static PlayerCursor Instance;

    private void Awake() {
        Instance = this;
    }

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MAXRAYLENGTH, layerMask)) {
            if (hit.collider.TryGetComponent<Item>(out Item item)) {
                HandleItems(item);
            } else {
                currentItem = null;
                SwitchCursor(0.1f);
            }

        } else {
            currentItem = null;
            SwitchCursor(0.1f);
        }


        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f) {
            SwitchCursor(scrollInput);
        }
    }

    void HandleItems(Item item) {
        bool updateCursor = currentItem == null;

        currentItem = item;

        if (updateCursor)
            SwitchCursor(0.1f);  
    }

    void SwitchCursor(float scrollDirection) {

        int totalCursors = Enum.GetValues(typeof(CursorState)).Length;

        if(currentItem == null) {
            SetCursorToNoneAndUpdate();
            return;
        }

        List<CursorState> cursorStates = new List<CursorState>();
        if(currentItem.itemData.lookable) { cursorStates.Add(CursorState.Look); }
        if (currentItem.itemData.talkable) { cursorStates.Add(CursorState.Talk); }
        if (currentItem.itemData.takeable) { cursorStates.Add(CursorState.Take); }

        int currentCursorIndex = cursorStates.IndexOf(currentCursorState);

        int totalValidCursors = cursorStates.Count;

        if (totalValidCursors == 0) {
            SetCursorToNoneAndUpdate();
            return;
        }

        if (scrollDirection > 0f) {
            // Scroll up, switch to next weapon
            currentCursorIndex = (currentCursorIndex + 1) % totalValidCursors;
        }
        else if (scrollDirection < 0f) {
            // Scroll down, switch to previous weapon
            currentCursorIndex = (currentCursorIndex - 1 + totalValidCursors) % totalValidCursors;
        }

        currentCursorState = cursorStates[currentCursorIndex];
        OnCursorChange.Invoke();

        void SetCursorToNoneAndUpdate() {
            currentCursorState = CursorState.None;
            OnCursorChange.Invoke();
            return;
        }
    }
}


public enum CursorState {
    None,
    Look,
    Talk,
    Take
}
