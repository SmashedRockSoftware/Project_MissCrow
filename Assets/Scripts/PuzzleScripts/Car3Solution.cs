using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Car3Solution : MonoBehaviour
{
    bool isColdTime = false;
    bool isFireBurning = false;

    [SerializeField] UnityEvent OnSolved;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)) { OnSolved.Invoke(); }
    }

    public void SetColdTemp() {
        isColdTime = true;
    }

    public void LightFire() {
        isFireBurning = true;
    }

    public void PressDial() {
        if (isColdTime && isFireBurning) {
            OnSolved.Invoke();
        }
        else {
            if (!isColdTime)
                DialogueUI.Instance.DisplayGrannyText("I don't think the waters cold enough");
            else if (!isFireBurning)
                DialogueUI.Instance.DisplayGrannyText("Somethings missing, its not cold enough, whats that hanging thing?");
        }
    }
}
