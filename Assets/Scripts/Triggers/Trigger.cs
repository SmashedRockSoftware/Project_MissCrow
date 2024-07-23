using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour {
    [Tooltip("Only objects with this tag will trigger this trigger, leave blank to allow anything to trigger this trigger")]
    [SerializeField] private protected string requiredTag = "Player";  //The tag we want to see to trigger
    [Tooltip("Unity event, which will execute when the required tag enters the collider")]
    [SerializeField] private protected UnityEvent OnEnter;
    [Tooltip("Unity event, which will execute when the required tag exits the collider")]
    [SerializeField] private protected UnityEvent OnExit;

    [Tooltip("Delays the event firing after entering for X seconds")]
    [SerializeField] private float delayEnter;
    [Tooltip("Delays the event firing after exiting for X seconds")]
    [SerializeField] private float delayExit;

    private void Start() {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (requiredTag == "") {
            Invoke(nameof(CallEventEnter), delayEnter);
        }
        else if (other.CompareTag(requiredTag)) {
            Invoke(nameof(CallEventEnter), delayEnter);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (requiredTag == "") {
            Invoke(nameof(CallEventExit), delayExit);
        }
        else if (other.CompareTag(requiredTag)) {
            Invoke(nameof(CallEventExit), delayExit);
        }
    }

    void CallEventEnter() {
        OnEnter.Invoke();
    }

    void CallEventExit() {
        OnExit.Invoke();
    }
}

