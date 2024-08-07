using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlidingDoorAnimation : MonoBehaviour
{
    [SerializeField] Transform doorTransform;
    [SerializeField] Vector3 openPosition;
    Vector3 closePosition;
    [SerializeField] float duration = 1f;
    [SerializeField] float openTime = 10f;

    [Space]
    [SerializeField] AudioSource audioSource;
    [SerializeField] float openingPitch = 1f;
    [SerializeField] float closingPitch = 0.8f;

    bool isOpen = false;

    Tween tween;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        closePosition = doorTransform.position;
        CloseDoor();
    }

    private void Update() {
        if(Input.GetKey(KeyCode.L)) {
            OpenDoor();
        }
    }

    public void OpenDoor() {
        if (isOpen) { return; }

        tween?.Kill(true);
        isOpen = true;
        tween = doorTransform.DOMove(closePosition - openPosition, duration);

        Invoke(nameof(CloseDoor), openTime);

        PlayDoorSlidingSound(openingPitch);
    }

    private void PlayDoorSlidingSound(float pitch = 1f) {
        if (audioSource && !audioSource.isPlaying) { 
            audioSource.pitch = pitch;
            audioSource.Play();
        }
    }

    public void CloseDoor() {
        if (!isOpen) { return; }

        tween?.Kill(true);
        isOpen = false;
        tween = doorTransform.DOMove(closePosition, duration);

        PlayDoorSlidingSound(closingPitch);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + closePosition - openPosition, 0.15f);
        Gizmos.DrawWireSphere(transform.position + closePosition, 0.15f);
        Gizmos.DrawLine(transform.position + closePosition - openPosition, transform.position + closePosition);
    }
}
