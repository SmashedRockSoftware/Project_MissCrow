using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoverNameUI : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI text1;
    //[SerializeField] TextMeshProUGUI text2;
    //[SerializeField] TextMeshProUGUI text3;
    //[SerializeField] TextMeshProUGUI text4;

    public static HoverNameUI instance;
    Camera cam;

    [SerializeField] Vector3[] offsets = {new Vector3(0, -50, 0), new Vector3(0, -70, 0), new Vector3(0, -90, 0), new Vector3(0, -110, 0), };

    Item item1;
    Item item2;

    [SerializeField] TextMeshProUGUI actionText;
    List<TextMeshProUGUI> actionTextList = new List<TextMeshProUGUI>();

    [SerializeField] List<Item> items = new List<Item>();

    string[] allActions = { "inspect", "pickup", "talk to", "use" };
    [SerializeField] List<string> actions = new List<string>();

    [SerializeField] int currentIndex;
    int maxIndex = 3;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        HideHoverText();
    }

    // Update is called once per frame
    void Update() {
        ////PositionTextObject(text1, offset1);
        ////PositionTextObject(text2, offset2);
        ////PositionTextObject(text3, offset3);
        ////PositionTextObject(text4, offset4);

        //for (int i = 0; i < actions.Count; i++) {
        //    PositionTextObject(actionTextList[i], offsets[i]);
        //}


        //int maxIndex = actions.Count - 1;

        //float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        //if (scrollInput != 0f) {
        //    if (scrollInput > 0f) {
        //        currentIndex++;
        //        if (currentIndex > maxIndex) { currentIndex = 0; }
        //    }
        //    else if (scrollInput < 0f) {
        //        currentIndex--;
        //        if(currentIndex < 0) { currentIndex = maxIndex; }
        //    }

        //    //FillNames(currentIndex);
        //}
    }

    private static void PositionTextObject(TextMeshProUGUI textObject, Vector3 offset) {
        if (textObject.gameObject.activeInHierarchy) {
            textObject.transform.position = Input.mousePosition + offset;
        }
    }

    public void ShowHoverText(string text) {
        //text1.gameObject.SetActive(true);
        //text1.text = text;
    }

    public void HideHoverText() {
        //text1.gameObject.SetActive(false);    
    }

    public void HoverOverItem(Item item) {
        ////text1.gameObject.SetActive(true);
        //items.Add(item);

        //if (items.Count == 1) {
        //    actions.Clear();
        //    if (item.gameObject.TryGetComponent<InspectAction>(out InspectAction inspectAction)) { actions.Add(allActions[0]); }
        //    if (item.gameObject.TryGetComponent<PickupAction>(out PickupAction pickupAction)) { actions.Add(allActions[1]); }
        //    if (item.gameObject.TryGetComponent<TalkAction>(out TalkAction talkAction)) { actions.Add(allActions[2]); }
        //    if (item.gameObject.TryGetComponent<UseAction>(out UseAction useAction)) { actions.Add(allActions[3]); }

        //    for (int i = 0; i < actionTextList.Count; i++) {
        //        Destroy(actionTextList[i]);
        //    }
        //    actionTextList.Clear();

        //    for (int i = 0; i < actions.Count; i++) {
        //        string action = actions[i];
        //        var actText = Instantiate(actionText, transform);
        //        actText.text = action.ToString();
        //        actionTextList.Add(actText);
        //        PositionTextObject(actText, offsets[i]);
        //    }

        //}
        //else if (items.Count == 2) {
        //    //text1.text = "Combine " + items[0].itemData.itemName + items[1].itemData.itemName;
        //}
    }

    private void FillNames(int _currentIndex) {
        //int maxIndex = actions.Count - 1;
        //if (_currentIndex > maxIndex) { _currentIndex = 0; }
        //if (_currentIndex < 0) { _currentIndex = maxIndex; }

        //if (actions.Count > 0) {
        //    text1.text = actions[_currentIndex] + " " + items[0].itemData.itemName;
        //}
        //else text1.gameObject.SetActive(false);

        //if (actions.Count > 1) {
        //    var action2 = currentIndex + 1;
        //    if (action2 > maxIndex) { action2 = 0; }
        //    if (action2 < 0) { action2 = maxIndex; }
        //    text2.text = actions[action2];
        //}
        //else text2.gameObject.SetActive(false);

        //if (actions.Count > 2) {
        //    var action3 = currentIndex + 2;
        //    if (action3 > maxIndex) { action3 = 0; }
        //    if (action3 < 0) { action3 = maxIndex; }
        //    text3.text = actions[action3];
        //}
        //else text3.gameObject.SetActive(false);

        //if (actions.Count > 3) {
        //    var action4 = currentIndex + 3;
        //    if (action4 > maxIndex) { action4 = 0; }
        //    if (action4 < 0) { action4 = maxIndex; }
        //    text4.text = actions[3];
        //}
        //else text4.gameObject.SetActive(false);
    }

    public void leaveOverItem(Item item) {
        //items.Remove(item);

        //if(items.Count == 0 ) {
        //    //text1.gameObject.SetActive(false);
        //}
    }
}
