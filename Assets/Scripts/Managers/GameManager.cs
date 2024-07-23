using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event System.Action OnPaused;
    public static event System.Action OnUnPaused;
    public static event System.Action OnCursorChange;

    bool LOCKEDCURSORVISIBILTY = true;
    CursorLockMode LOCKEDCURSORLOCKEDMODE = CursorLockMode.Confined;

    bool UNLOCKEDCURSORVISIBILTY = true;
    CursorLockMode UNLOCKEDCURSORLOCKEDMODE = CursorLockMode.None;

    public GameState currentGameState = GameState.InGame;
    public CursorState currentCursorState = CursorState.Look;

    public static GameManager Instance;

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UnPauseGame() {
        currentGameState = GameState.InGame;
        OnUnPaused.Invoke();
        LockCursor();
    }

    public void PauseGame() {
        currentGameState = GameState.Paused;
        OnPaused.Invoke();
        UnlockCursor();
    }

    public void UnlockCursor() {
        Cursor.lockState = UNLOCKEDCURSORLOCKEDMODE;
        Cursor.visible = UNLOCKEDCURSORVISIBILTY;
    }

    public void LockCursor() {
        Cursor.lockState = LOCKEDCURSORLOCKEDMODE;
        Cursor.visible = LOCKEDCURSORVISIBILTY;
    }

    // Update is called once per frame
    void Update()
    {
        //float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        //if (scrollInput != 0f) {
        //    SwitchCursor(scrollInput);
        //}
    }

    //void SwitchCursor(float scrollDirection) {
    //    int currentCursorIndex = (int)currentCursorState;
    //    int totalCursors = Enum.GetValues(typeof(CursorState)).Length;

    //    if (scrollDirection > 0f) {
    //        // Scroll up, switch to next cursor
    //        currentCursorIndex = (currentCursorIndex + 1) % totalCursors;
    //    }
    //    else if (scrollDirection < 0f) {
    //        // Scroll down, switch to previous cursor
    //        currentCursorIndex = (currentCursorIndex - 1 + totalCursors) % totalCursors;
    //    }

    //    currentCursorState = (CursorState)currentCursorIndex;
    //    OnCursorChange.Invoke();
    //}
}

public enum GameState {
    InGame,
    Paused
}

//public enum CursorState {
//    None,
//    Look,
//    Talk,
//    Take
//}

