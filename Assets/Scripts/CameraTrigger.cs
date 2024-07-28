using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using Unity.VisualScripting;

public class CameraTrigger : MonoBehaviour {

    [Tooltip("Only objects with this tag will trigger this trigger, leave blank to allow anything to trigger this trigger")]
    [SerializeField] private string requiredTag = "Player";  //The tag we want to see to trigger

    [Tooltip("The Camera that will be active when we are triggered, If left empty we will attempt to get a virtual camera in a child object at start.")]
    [SerializeField] private CinemachineVirtualCamera Camera;

    [SerializeField] bool isDebug = false;

    private void Start() {
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        if (Camera == null) Camera = gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter(Collider other) {
        if (requiredTag == "" || other.CompareTag(requiredTag)) {

            if (Camera == null) return;

            if(isDebug) Debug.Log("Set the visible Camera to " + Camera.name);
            CameraManager.instance.SetCameraToVisible(Camera);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (requiredTag == "" || other.CompareTag(requiredTag)) {

            if (Camera == null) return;

            // Debug.Log("Set the visible Camera to " + Camera.name);
            // CameraManager.instance.SetCameraToVisible(Camera);
        }
    }

    public void TriggerCameraChange() {

        if (Camera == null) return;

        if (isDebug) Debug.Log("Set the visible Camera to " + Camera.name);
        CameraManager.instance.SetCameraToVisible(Camera);
    }

    private void OnDrawGizmos() {
        if (Camera == null) return;
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, Camera.transform.position);
    }

    private void OnDrawGizmosSelected() {
        if (Camera == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, Camera.transform.position);
    }
}

