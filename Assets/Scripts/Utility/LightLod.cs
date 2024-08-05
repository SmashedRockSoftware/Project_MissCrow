using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LightLod : MonoBehaviour
{
    Light light;
    Camera cam;

    [SerializeField] float distanceToDisable = 20f;
    [SerializeField] float distanceToShadows = 15f;
    //[SerializeField] float distanceLod2 = 30f;

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

            if(distance < distanceToDisable) {
                light.enabled = true;

                if (distance < distanceToShadows) {
                    light.shadows = LightShadows.Soft;
                } else {
                    light.shadows = LightShadows.None;
                }

            } else {
                disableLight();
            }
        }
    }

    private void setLightToMiddleSetting() {
        light.enabled = true;
        light.shadowResolution = LightShadowResolution.Low;
        light.shadows = LightShadows.Hard;
    }

    private void setLightToHighest() {
        light.enabled = true;
        light.shadowResolution = LightShadowResolution.High;
        light.shadows = lightShadowsSetting;
    }

    private void disableLight() {
        light.enabled = false;
        light.shadowResolution = LightShadowResolution.Low;
        light.shadows = LightShadows.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceToDisable);

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, distanceLod2);
    }
}
