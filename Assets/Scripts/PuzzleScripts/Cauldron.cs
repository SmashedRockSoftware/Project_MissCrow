using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cauldron : MonoBehaviour
{
    [SerializeField] UnityEvent OnSolved;
    [SerializeField] UnityEvent OnFailed;

    [SerializeField] List<PotionElement> Elements = new List<PotionElement>();
    [SerializeField] List<PotionElement> SolutionElements = new List<PotionElement>();

    [SerializeField] ItemScriptableObject rareMud;
    [SerializeField] ItemScriptableObject sunPowder;
    [SerializeField] ItemScriptableObject loonShale;
    [SerializeField] ItemScriptableObject firePoker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SolvePuzzle() {
        OnSolved.Invoke();
    }

    public void FailedPuzzle() {
        OnFailed.Invoke();
        Debug.Log("Failed to get the right ingredients");
        DialogueUI.Instance.DisplayGrannyText("Gee, that was dangerous, better see if something can help me with this potion");
        Elements.Clear();
    }

    public void WhackPotion() {
        bool goodPotion = true;

        if (Elements.Count != SolutionElements.Count) { FailedPuzzle(); return; }

        goodPotion = CheckIfCorrectIngredients();

        if (goodPotion) {
            SolvePuzzle();
        }
        else {
            FailedPuzzle();
        }

        Elements.Clear();
    }

    private bool CheckIfCorrectIngredients() {
        bool goodPotion = true;
        for (int i = 0; i < Elements.Count; i++) {
            if (Elements[i].typeOfElement != SolutionElements[i].typeOfElement) {
                goodPotion = false;
                break;
            }
        }

        return goodPotion;
    }

    public void DroppedOn(GameObject objectDropped) {
        AddElementToPotion(objectDropped.GetComponent<Item>().itemData);
    }

    public void AddElementToPotion(ItemScriptableObject data) {
        if(data == null) { Debug.LogError("no item data for dropped item on cauldron"); return; }

        if (data == firePoker) {
            WhackPotion();
            return;
        }

        var elemnt = new PotionElement();

        if (data == rareMud)
            elemnt.typeOfElement = PotionElementTypes.rareMud;
        else if (data == sunPowder)
            elemnt.typeOfElement = PotionElementTypes.sunPowder;
        else if (data == loonShale)
            elemnt.typeOfElement = PotionElementTypes.loonShale;
        else
            elemnt.typeOfElement = PotionElementTypes.wrong;

        Elements.Add(elemnt);

        if (!CheckIfCorrectIngredients()) {
            FailedPuzzle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)) {
            SolvePuzzle();
        }
    }
}

[System.Serializable]
public class PotionElement {
    public PotionElementTypes typeOfElement;
}

public enum PotionElementTypes {
    rareMud,
    sunPowder,
    loonShale,
    wrong
}
