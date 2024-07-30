using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTargets : MonoBehaviour
{
    public bool isDropable = true;

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

    public void RemoveDropTarget() {
        FindObjectOfType<InventoryUI>().drop
        Destroy(this);
    }
}
