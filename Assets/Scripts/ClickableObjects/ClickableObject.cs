using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class ClickableObject : MonoBehaviour
{
    public Transform locationOverride;
    [SerializeField] UnityEvent OnClicked;

    void Start() {

    }

    // The mesh goes red when the mouse is over it...
    void OnMouseEnter() {

    }

    // ...the red fades out to cyan as the mouse is held over...
    void OnMouseOver() {
        if(Input.GetMouseButtonDown(0)) {
            OnClicked.Invoke();
        }
    }

    // ...and the mesh finally turns white when the mouse moves away.
    void OnMouseExit() {

    }
}
