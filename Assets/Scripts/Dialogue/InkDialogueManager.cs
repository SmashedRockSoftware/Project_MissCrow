using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.Collections;
using UnityEngine.EventSystems;

public class InkDialogueManager : MonoBehaviour {
    [SerializeField] DialogueUI dialogueUI;

    public Story story;
    bool isLocked;

    public static event System.Action<string> OnDialogueDisplayed;

    // Start is called before the first frame update
    void Start() {
        if(dialogueUI == null) dialogueUI = FindObjectOfType<DialogueUI>();
        //dialogueUI?.gameObject.SetActive(false);
    }

    private void Update() {
        if (story != null && DialogueKey()) {
            NextDialougue();
        }
    }

    bool DialogueKey() {
        return ((Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject()) || Input.GetKeyDown(KeyCode.Space)) || Input.GetKey(KeyCode.LeftControl);
    }

    public void LockAndWait(float wait) {
        StartCoroutine(LockAndWaitCoroutine(wait));
    }

    public IEnumerator LockAndWaitCoroutine(float wait) {
        isLocked = true;
        dialogueUI.NextButtonVisibility(false);
        //yield return Helpers.GetWait(wait);
        yield return new WaitForSeconds(wait);
        dialogueUI.NextButtonVisibility(true);
        isLocked = false;
    }

    public void LockAndNext(float wait) {
        StartCoroutine(LockAndNextCoroutine(wait));
    }

    public IEnumerator LockAndNextCoroutine(float wait) {
        isLocked = true;
        dialogueUI.NextButtonVisibility(false);
        //yield return Helpers.GetWait(wait);
        yield return new WaitForSeconds(wait);
        dialogueUI.NextButtonVisibility(true);
        isLocked = false;
        NextDialougue();
    }

    public virtual void StartDialogue(TextAsset dialogueTextFile) {
        story = new Story(dialogueTextFile.text);

        NextDialougue();
    }

    public virtual void CloseDialogue() {
        //GameManager.instance.ExitDialogue(); todo
        //dialogueUI.gameObject.SetActive(false);
        dialogueUI.ExitDialogue();

        //CamManager.SetVisibleCamera("Virtual Camera"); todo

        story = null;
    }

    private void OnClickChoiceButton(Choice choice) {
        // Handle the choice selection logic here
        story.ChooseChoiceIndex(choice.index);
        dialogueUI.RemoveAllChoiceButtons();
        NextDialougue(); // Continue to the next part of the dialogue
    }

    public virtual void NextDialougue() {
        if (isLocked) return;

        string nextDialogue = "";
        bool lastLine = false;

        dialogueUI.SetTextVisibility(true);
        dialogueUI.NextButtonVisibility(true);
        dialogueUI.RemoveAllChoiceButtons();

        // Read all the content until we can't continue any more
        if (story.canContinue) {
            // Continue gets the next line of the story
            string text = story.Continue();

            // This removes any white space from the text.
            text = text.Trim();

            // Display the text on screen!
            nextDialogue += text;

            // Process tags associated with the current line
            List<string> tags = story.currentTags;
            if (tags != null && tags.Count > 0) {
                foreach (string tag in tags) {
                    // Process each tag here
                    TagProcessor.ProcessInkTag(tag);
                }
            }
        }
        else {
            CloseDialogue();
            return;
        }

        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0) {
            for (int i = 0; i < story.currentChoices.Count; i++) {
                Choice choice = story.currentChoices[i];
                dialogueUI.AddChoiceButton(choice.text, (choiceToSelect) => { OnClickChoiceButton(choiceToSelect); }, choice);
            }
        }

        OnDialogueDisplayed?.Invoke(nextDialogue);
        dialogueUI.DisplayDialogue(nextDialogue, story.canContinue);
    }
}
