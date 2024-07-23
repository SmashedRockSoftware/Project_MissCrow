using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using DG.Tweening;
using static UnityEditor.FilePathAttribute;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform m_PlayerLocationTarget;
    NavMeshAgent agent;

    [SerializeField] LayerMask layerMask = ~0;
    Camera m_Camera;

    const float MAXRAYLENGTH = 100f;

    [SerializeField] bool requireClickableForMovement = true;
    float origSpeed;

    public static event System.Action OnGoto;

    bool stillMoving;

    const float minDistanceBeforeCheckingIfMoving = 10f;
    [SerializeField] float rotateSpeed = 15f;
    [SerializeField] float duration = 1f;
    Tween rotateToFace;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;
        agent = gameObject.GetComponent<NavMeshAgent>();
        origSpeed = agent.speed;
    }

    public void UnPauseGame() {
        agent.speed = origSpeed;
    }

    public void PauseGame() {
        agent.speed = 0;
    }

    private void OnEnable() {
        GameManager.OnPaused += PauseGame;
        GameManager.OnUnPaused += UnPauseGame;
    }

    private void OnDisable() {
        GameManager.OnPaused -= PauseGame;
        GameManager.OnUnPaused -= UnPauseGame;
    }

    private void CastRay() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MAXRAYLENGTH, layerMask)) {
            if (hit.collider.TryGetComponent<ClickableObject>(out ClickableObject clickableObject)) {
                GoToOverideOrHit(hit, clickableObject);
            }
            else if(!requireClickableForMovement) {
                GoTo(hit.point);
            }
        }

        void GoToOverideOrHit(RaycastHit hit, ClickableObject clickableObject) {
            if (clickableObject.locationOverride != null) {
                GoTo(clickableObject.locationOverride);
            }
            else {
                GoTo(hit.point);
            }
        }
    }

    public void GoTo(Transform _transform) {
        m_PlayerLocationTarget.rotation = _transform.rotation;
        GoTo(_transform.position);
    }

    public void GoTo(Vector3 _location) {
        m_PlayerLocationTarget.position = _location;
        agent.SetDestination(m_PlayerLocationTarget.position);
        OnGoto.Invoke();

        //stillMoving = true;
        //rotateToFace = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            CastRay();
        }

        //if (stillMoving && agent.velocity.magnitude <= 0 && Vector3.Distance(agent.transform.position, agent.destination) < minDistanceBeforeCheckingIfMoving) {
        //    stillMoving = false;
        //}

        //if (!stillMoving && rotateToFace == null) {
        //    Vector3 comboPosition = m_PlayerLocationTarget.position + m_PlayerLocationTarget.forward;  //Get the facing on the location override
        //    Vector3 position = new Vector3(comboPosition.x, transform.position.y, comboPosition.z);  //Cancel vertical rotation
        //    rotateToFace = transform.DOLookAt(position, duration);
        //}
    }

    [SerializeField] float length = 1f;

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(m_PlayerLocationTarget.position, m_PlayerLocationTarget.position + (m_PlayerLocationTarget.forward * length));
    }
}
