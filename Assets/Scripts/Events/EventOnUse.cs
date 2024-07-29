using UnityEngine;
using UnityEngine.Events;

public class EventOnUse : MonoBehaviour {
    public ItemScriptableObject requiredItem;
    [SerializeField] UnityEvent OnUse;

    public void OnUseFireEvent() {
        OnUse.Invoke();
    }
}

