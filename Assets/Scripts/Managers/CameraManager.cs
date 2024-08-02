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

    public void SetCameraToVisible(CinemachineVirtualCamera wantedCamera) {
        //var wantedCamera = obj.GetComponent<CinemachineVirtualCamera>();
        if (wantedCamera == null) return;

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
