using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontRender : MonoBehaviour
{
    [SerializeField] Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        if(rend == null) { rend.gameObject.GetComponent<Renderer>(); }
        rend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
