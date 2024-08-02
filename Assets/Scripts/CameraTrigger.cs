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
    [SerializeField] private CinemachineVirtualCamera cam;

    [SerializeField] bool isDebug = false;

    private void Start() {
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        if (cam == null) cam = gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter(Collider other) {
        if (requiredTag == "" || other.CompareTag(requiredTag)) {

            if (cam == null) return;

            if(isDebug) Debug.Log("Set the visible Camera to " + cam.name);
            CameraManager.instance.SetCameraToVisible(cam);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (cam == CameraManager.instance.currentCamera) return;

        if (requiredTag == "" || other.CompareTag(requiredTag)) {

            if (cam == null) return;

            if (isDebug) Debug.Log("Set the visible Camera to " + cam.name);
            CameraManager.instance.SetCameraToVisible(cam);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (requiredTag == "" || other.CompareTag(requiredTag)) {

            if (cam == null) return;

            // Debug.Log("Set the visible Camera to " + Camera.name);
            // CameraManager.instance.SetCameraToVisible(Camera);
        }
    }

    public void TriggerCameraChange() {

        if (cam == null) return;

        if (isDebug) Debug.Log("Set the visible Camera to " + cam.name);
        CameraManager.instance.SetCameraToVisible(cam);
    }

    private void OnDrawGizmos() {
        if (cam == null) return;
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, cam.transform.position);
    }

    private void OnDrawGizmosSelected() {
        if (cam == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, cam.transform.position);
    }
}

