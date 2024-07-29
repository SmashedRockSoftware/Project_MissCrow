using UnityEngine;
using UnityEngine.Events;

public class EventOnPickup : MonoBehaviour {
    public ItemScriptableObject requiredItem;
    [SerializeField] UnityEvent OnPickup;

    public void OnPickupFireEvent() {
        OnPickup.Invoke();
    }
}

