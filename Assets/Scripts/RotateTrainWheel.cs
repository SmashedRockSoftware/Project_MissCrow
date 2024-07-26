using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrainWheel : MonoBehaviour
{
    [SerializeField] private float angle = 5f;
    [SerializeField] private Vector3 axis = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(axis, angle * Time.deltaTime);
    }
}
