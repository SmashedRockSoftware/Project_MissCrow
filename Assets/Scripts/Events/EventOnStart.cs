using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnStart : MonoBehaviour
{
    [SerializeField] UnityEvent OnStart;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        OnStart.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
