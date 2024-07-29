using UnityEngine;
using UnityEngine.Events;

public class EventOnCombo : MonoBehaviour
{
    public Combination requiredCombo;
    [SerializeField] UnityEvent OnCombination;

    public void OnCombinationFireEvent() {
        OnCombination.Invoke();
    }
}
