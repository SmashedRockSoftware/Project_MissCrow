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

    bool isOpen = false;

    Tween tween;

    // Start is called before the first frame update
    void Start()
    {
        closePosition = doorTransform.position;
        CloseDoor();
    }

    public void OpenDoor() {
        tween?.Kill(true);
        isOpen = true;
        tween = doorTransform.DOMove(openPosition, duration);

        Invoke(nameof(CloseDoor), openTime);
    }

    public void CloseDoor() {
        tween?.Kill(true);
        isOpen = false;
        tween = doorTransform.DOMove(closePosition, duration);
    }
}
