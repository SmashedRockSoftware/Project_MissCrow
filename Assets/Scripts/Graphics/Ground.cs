using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] Vector3 startPoint = new Vector3(-7.5f, -0.5469999f, -0.1525998f);
    [SerializeField] float restartIfValue = 10f;
    float origSpeed;

    private void Start() {
        origSpeed = speed;
    }

    public void UnPauseGame() {
        speed = origSpeed;
    }

    public void PauseGame() {
        speed = 0f;
    }

    private void OnEnable() {
        GameManager.OnPaused += PauseGame;
        GameManager.OnUnPaused += UnPauseGame;
    }

    private void OnDisable() {
        GameManager.OnPaused -= PauseGame;
        GameManager.OnUnPaused -= UnPauseGame;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);

        Debug.Log("transform.localPosition.z " + transform.localPosition.z + " restartIfValue " + restartIfValue);

        if(transform.localPosition.z > restartIfValue) {
            transform.localPosition = startPoint;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(startPoint, startPoint + (Vector3.up * 1000f));
        //Gizmos.DrawLine(new Vector3, startPoint + (Vector3.up * 1000f));
    }
}
