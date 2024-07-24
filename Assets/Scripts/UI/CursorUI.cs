using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorUI : MonoBehaviour
{
    public Texture2D[] cursorTextures;
    [SerializeField] int cursorIndex;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private void OnEnable() {
        PlayerCursor.OnCursorChange += OnCursorChange;
    }

    private void OnDisable() {
        PlayerCursor.OnCursorChange -= OnCursorChange;
    }

    // Start is called before the first frame update
    void Start() {
        OnCursorChange();
    }

    private void OnCursorChange() {
        Cursor.SetCursor(cursorTextures[(int)PlayerCursor.Instance.currentCursorState], hotSpot, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
