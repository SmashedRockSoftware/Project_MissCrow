using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class FireFlicker : MonoBehaviour
{
    [SerializeField] int framesBetweenUpdates = 4;
    [SerializeField] int varianceBetweenUpdates = 2;

    Light m_light;


    float m_range;
    [SerializeField] float rangeRange = 5f;

    float m_intensity;
    [SerializeField] float intensityRange = 20f;

    Vector3 m_position;
    [SerializeField] float positionRange = 0.015f;

    int randomFrameWait = 4;

    // Start is called before the first frame update
    void Start()
    {
        m_light = GetComponent<Light>();

        m_intensity = m_light.intensity;
        m_range = m_light.intensity;
        m_position = m_light.transform.position;

        StartCoroutine(UpdateFlame());
    }
     
    IEnumerator UpdateFlame() {
        while (true) {
            for (int i = 0; i < randomFrameWait; i++) {
                yield return new WaitForEndOfFrame();
            }

            m_light.range = m_range + Random.Range(-rangeRange, rangeRange);
            m_light.intensity = m_intensity + Random.Range(-intensityRange, intensityRange);
            var newPos = new Vector3(m_position.x + Random.Range(-positionRange, positionRange), m_position.y + Random.Range(-positionRange, positionRange), m_position.z + Random.Range(-positionRange, positionRange));
            m_light.transform.position = newPos;

            randomFrameWait = Random.Range(framesBetweenUpdates - varianceBetweenUpdates, framesBetweenUpdates + varianceBetweenUpdates);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
