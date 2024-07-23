using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform m_PlayerLocationTarget;
    NavMeshAgent agent;

    [SerializeField] LayerMask m_layerMask = ~0;
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
        if (Physics.Raycast(ray, out hit, MAXRAYLENGTH, m_layerMask)) {
            if (hit.collider.TryGetComponent<ClickableObject>(out ClickableObject clickableObject)) {
                GoToOverideOrHit(hit, clickableObject);
            }
            else if(!requireClickableForMovement) {
                GoTo(hit.point);
            }
        }

        void GoToOverideOrHit(RaycastHit hit, ClickableObject clickableObject) {
            if (clickableObject.locationOverride != null) {
                GoTo(clickableObject.locationOverride.position);
            }
            else {
                GoTo(hit.point);
            }
        }
    }

    public void GoTo(Transform _transform) {
        GoTo(_transform.position);
    }

    public void GoTo(Vector3 _location) {
        m_PlayerLocationTarget.position = _location;
        agent.SetDestination(m_PlayerLocationTarget.position);
        OnGoto.Invoke();

        stillMoving = true;
        rotateToFace = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            CastRay();
        }

        if (agent.velocity.magnitude <= 0 && Vector3.Distance(agent.transform.position, agent.destination) < minDistanceBeforeCheckingIfMoving) {
            stillMoving = false;
        }

        if (!stillMoving && rotateToFace == null) {
            rotateToFace = transform.DOLookAt(new Vector3(m_PlayerLocationTarget.position.x, transform.position.y, m_PlayerLocationTarget.position.z), duration);
        }
    }
}
