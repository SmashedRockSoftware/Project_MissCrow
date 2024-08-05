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
    bool preventCameraChange;

    // Start is called before the first frame update
    private void Start() {
        instance = this;

        virtualCameras = FindObjectsOfType(typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera[];
    }

    public void SetLockedCameraToVisible(CinemachineVirtualCamera wantedCamera) {
        SetCameraToVisible(wantedCamera);
        preventCameraChange = true;
    }

    public void UnlockCamera() {
        preventCameraChange = false;
    }

    public void SetCameraToVisible(CinemachineVirtualCamera wantedCamera) {
        if (preventCameraChange) return;
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
