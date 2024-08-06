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

    public bool inTalkingMode;

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LockCursor();
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

    public void UnNotebookGame() {
        currentGameState = GameState.InGame;
        OnUnPaused.Invoke();
        LockCursor();
    }

    public void NotebookGame() {
        currentGameState = GameState.InNotebook;
        OnPaused.Invoke();
        UnlockCursor();
    }

    public void UnCutsceneGame() {
        currentGameState = GameState.InGame;
        //OnUnPaused.Invoke();
        //LockCursor();
    }

    public void CutsceneGame() {
        currentGameState = GameState.InCutscene;
        //OnPaused.Invoke();
        //UnlockCursor();
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
    }

    public void EnterTalkingMode(DialogueScript dialogueScript) {
        EnterTalkingMode(dialogueScript.startingVirtualCamera.transform, dialogueScript.item, dialogueScript.objectsToMoveToLayer, dialogueScript.scriptList);
    }

    public void EnterTalkingMode(Transform camera, Item item, GameObject[] objsToMove, List<string> dialogueScript) {
        inTalkingMode = true;
        currentGameState = GameState.InDialogue;
        DialogueUI.Instance.EnterDialogue(camera, item, objsToMove, dialogueScript);
    }

    public void ExitTalkingMode() {
        inTalkingMode = false;
        currentGameState = GameState.InGame;
    }
}

public enum GameState {
    InGame,
    InDialogue,
    InNotebook,
    InCutscene,
    Paused
}

