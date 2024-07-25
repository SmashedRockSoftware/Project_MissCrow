using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InspectAction : MonoBehaviour
{
    [HideInInspector] public ItemScriptableObject itemData;
    //[SerializeField] string grannyInspectDialogue;
    [SerializeField] UnityEvent onInspect;
    [SerializeField] string InpectionDialogue;

    // Start is called before the first frame update
    void Start()
    {
        itemData = GetComponent<Item>().itemData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInspectionText(string newInspectionText) {
        InpectionDialogue = newInspectionText;
    }

    public void Inspect() {
        if(InpectionDialogue == "")
            DialogueUI.Instance.DisplayGrannyText(itemData.inspectDialogue);
        else
            DialogueUI.Instance.DisplayGrannyText(InpectionDialogue);
        onInspect.Invoke();
    }
}
