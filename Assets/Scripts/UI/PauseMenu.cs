using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject UIPanel;

    public void OnUnPauseGame() {
        UIPanel.SetActive(false);
    }

    public void OnPauseGame() {
        UIPanel.SetActive(true);
    }



    private void OnEnable() {
        GameManager.OnPaused += OnPauseGame;
        GameManager.OnUnPaused += OnUnPauseGame;
    }

    private void OnDisable() {
        GameManager.OnPaused -= OnPauseGame;
        GameManager.OnUnPaused -= OnUnPauseGame;
    }

    public void UnPauseGame() {
        GameManager.Instance.UnPauseGame();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameManager.Instance.currentGameState == GameState.InGame) {
                GameManager.Instance.PauseGame();
            }
            else {
                GameManager.Instance.UnPauseGame();
            }
        }
    }
}
