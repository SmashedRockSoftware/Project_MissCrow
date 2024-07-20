using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler  {
    [SerializeField] Canvas canvas;
    Vector3 startingPosition;

    public void OnBeginDrag(PointerEventData eventData) {
        startingPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData) {
        PointerEventData pointerData = (PointerEventData)eventData;

        Vector2 postion;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerData.position, canvas.worldCamera, out postion);

        transform.position = canvas.transform.TransformPoint(postion);
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.position = startingPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.parent.GetComponentInParent<Canvas>();  //This is dumb and if the path changes this will break
        if(canvas == null) { Debug.LogError("DragAndDropUI::Start() | DragAndDropUI needs a reference to a canvas and failed to find it"); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
