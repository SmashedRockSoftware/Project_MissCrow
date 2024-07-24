using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InspectAction : MonoBehaviour
{
    public ItemScriptableObject itemData;
    //[SerializeField] string grannyInspectDialogue;
    [SerializeField] UnityEvent onInspect;

    // Start is called before the first frame update
    void Start()
    {
        itemData = GetComponent<Item>().itemData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Inspect() {
        DialogueUI.Instance.DisplayGrannyText(itemData.inspectDialogue);
        onInspect.Invoke();
    }
}
