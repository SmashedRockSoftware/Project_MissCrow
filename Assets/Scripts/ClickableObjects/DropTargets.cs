using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropTargets : MonoBehaviour
{
    public bool isDropable = true;
    [SerializeField] UnityEvent onDroppedOn;

    public void SetDropAllowance(bool allowance) {
        isDropable = allowance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DroppedOn() {
        onDroppedOn.Invoke();
    }

    public void RemoveDropTarget() {
        FindObjectOfType<InventoryUI>().RemoveDropTarget(this);
    }

    public void AddToDropTarget() {
        FindObjectOfType<InventoryUI>().AddDropTarget(this);
    }
}
