using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoceEnableOnStart : MonoBehaviour
{
    [SerializeField] GameObject forcedObject;

    // Start is called before the first frame update
    void Start()
    {
        forcedObject.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
