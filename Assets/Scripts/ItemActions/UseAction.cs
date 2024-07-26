using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UseAction : MonoBehaviour
{
    ItemScriptableObject itemData;
    [SerializeField] UnityEvent onUse;

    const string USESOUNDSTR = "use";

    // Start is called before the first frame update
    void Start()
    {
        //itemData = gameObject.GetComponent<ItemScriptableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseItem() {
        onUse.Invoke();

        //AudioAssistant.instance.PlayResourceSoundAtPoint(itemData, USESOUNDSTR, transform.position);
    }
}
