using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintToUseH : MonoBehaviour
{
    [SerializeField] float maxHintTime = 30f;
    [SerializeField] float timeTillHint = 5f;
    [SerializeField] float delayHintWhenPressed = 2f;
    [SerializeField] float timer;

    [SerializeField] GameObject hintText;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeTillHint;
    }

    // Update is called once per frame
    void Update()
    {
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
}
