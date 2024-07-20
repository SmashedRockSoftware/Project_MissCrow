using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event System.Action OnPaused;
    public static event System.Action OnUnPaused;

    bool LOCKEDCURSORVISIBILTY = true;
    CursorLockMode LOCKEDCURSORLOCKEDMODE = CursorLockMode.Confined;

    bool UNLOCKEDCURSORVISIBILTY = true;
    CursorLockMode UNLOCKEDCURSORLOCKEDMODE = CursorLockMode.None;

    public GameState currentGameState = GameState.InGame;

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

    }
}

public enum GameState {
    InGame,
    Paused
}
