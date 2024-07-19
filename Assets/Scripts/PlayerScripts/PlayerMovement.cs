using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform m_PlayerLocationTarget;
    NavMeshAgent m_agent;

    [SerializeField] LayerMask m_layerMask = ~0;
    Camera m_Camera;

    const float MAXRAYLENGTH = 100f;

    [SerializeField] bool requireClickableForMovement = true;
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;
        m_agent = gameObject.GetComponent<NavMeshAgent>();
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
        m_agent.SetDestination(m_PlayerLocationTarget.position);    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            CastRay();
        }
    }
}
