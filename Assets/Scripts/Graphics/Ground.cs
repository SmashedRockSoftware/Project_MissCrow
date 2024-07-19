using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] Vector3 startPoint = new Vector3(-7.5f, -0.5469999f, -0.1525998f);
    [SerializeField] float restartIfValue = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);

        if(transform.position.x > restartIfValue) {
            transform.position = startPoint;
        }
    }
}
