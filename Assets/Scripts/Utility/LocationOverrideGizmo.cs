using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationOverrideGizmo : MonoBehaviour
{
    [SerializeField] float length = 1f;

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * length));
    }
}
