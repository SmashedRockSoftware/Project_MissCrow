using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera[] virtualCameras;
    public static CameraManager instance;

    public delegate void CameraChangedDelegate(Transform camera);

    public CameraChangedDelegate CameraChanged;

    public CinemachineVirtualCamera currentCamera;
    bool canChange = true;

    // Start is called before the first frame update
    private void Start() {
        instance = this;

        virtualCameras = FindObjectsOfType(typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera[];
    }

    //public void SetCameraToHidden(GameObject obj) {
    //    foreach (var cam in virtualCameras) {
    //        if (cam == obj) {
    //            cam.gameObject.SetActive(false);
    //            break;
    //        }
    //    }
    //}

    public void ReleaseForcedCamera() {
        canChange = true;
    }

    public void SetCameraToVisible(CinemachineVirtualCamera wantedCamera, bool force = false) {
        //var wantedCamera = obj.GetComponent<CinemachineVirtualCamera>();
        if (!canChange) return;
        if (wantedCamera == null) return;

        if (force) {
            canChange = false;
        }

        CameraChanged?.Invoke(wantedCamera.transform);

        foreach (var cam in virtualCameras) {
            if (cam == null) continue;
            if (cam == wantedCamera) {
                currentCamera = cam;
                cam.gameObject.SetActive(true);
            }
            else
                cam.gameObject.SetActive(false);
        }
    }
}
