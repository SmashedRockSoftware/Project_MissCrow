using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropTargets : MonoBehaviour
{
    public bool isDropable = true;
    public bool shouldIgnoreCombo = false;
    [SerializeField] UnityEvent onDroppedOn;
    [SerializeField] UnityEvent onMovmentFinDroppedOn;

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

    public void DroppedOn(GameObject objectDropped) {
        onDroppedOn.Invoke();
    }

    public void MovementFinDroppedOn() {
        PlayerMovement.OnMovementFinishes += MoveFinished;
    }

    private void OnDisable() {
        PlayerMovement.OnMovementFinishes -= MoveFinished;
    }

    public void MoveFinished() {
        onMovmentFinDroppedOn.Invoke();
    }

    public void RemoveDropTarget() {
        FindObjectOfType<InventoryUI>().RemoveDropTarget(this);
    }

    public void AddToDropTarget() {
        FindObjectOfType<InventoryUI>().AddDropTarget(this);
    }
}
