using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LightLod : MonoBehaviour
{
    Light light;
    Camera cam;

    [SerializeField] float distanceLod1 = 15f;
    [SerializeField] float distanceLod2 = 30f;

    [SerializeField] float distance;

    LightShadows lightShadowsSetting = LightShadows.Soft;

    const float minWait = 0.1f, maxWait = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        cam = Camera.main;

        lightShadowsSetting = light.shadows;

        StartCoroutine(LODBasedOnDistance());
    }

    IEnumerator LODBasedOnDistance() {
        while (true) {
            yield return new WaitForSecondsRealtime(Random.Range(minWait, maxWait));
            distance = Vector3.Distance(cam.transform.position, transform.position);

            if (distance < distanceLod1) {
                light.enabled = true;
                light.shadowResolution = (LightShadowResolution)QualitySettings.shadowResolution;
                light.shadows = lightShadowsSetting;
            }
            else if (distance < distanceLod2) {
                light.enabled = true;
                light.shadowResolution = LightShadowResolution.Low;
                light.shadows = LightShadows.Hard;
            }
            else {
                light.enabled = false;
                light.shadowResolution = LightShadowResolution.Low;
                light.shadows = LightShadows.None;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
