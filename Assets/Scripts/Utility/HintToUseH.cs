using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintToUseH : MonoBehaviour
{
    private const string HSTRHELPER = "Need help? Hold \"H\" to highlight everything in a puzzle.";
    private const string INTERHELPER = "Click to interact";
    [SerializeField] float maxHintTime = 30f;
    [SerializeField] float timeTillHint = 5f;
    [SerializeField] float delayHintWhenPressed = 2f;
    [SerializeField] float timer;

    [SerializeField] TextMeshProUGUI hintText;

    [Space]
    [SerializeField] GameObject comboHint;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeTillHint;
        hintText.text = INTERHELPER;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.currentGameState != GameState.InGame) {
            hintText.gameObject.SetActive(false);
            return;
        }

        if(Input.GetMouseButtonDown(0) && hintText.text == INTERHELPER) {
            hintText.text = HSTRHELPER;
        }

        timer -= Time.deltaTime;

        hintText.gameObject.SetActive(timer < 0);

        if (Input.anyKeyDown) {
            if(timer < timeTillHint / 2)
                timer = timeTillHint/2;
        }

        if (Input.GetKeyDown(KeyCode.H)) {
            timeTillHint += delayHintWhenPressed;

            if(timeTillHint > maxHintTime) 
                timeTillHint = maxHintTime;

            timer = timeTillHint;
        }
    }

    public void DragAndDropComboHint() {
        comboHint.SetActive(true);
    }
}
