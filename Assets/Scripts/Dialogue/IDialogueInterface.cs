using System;
using Ink.Runtime;

public interface IDialogueInterface {
    void DisplayDialogue(string dialogue, bool canContinue);
    void NextButtonVisibility(bool isVisible);
    void SetTextVisibility(bool isVisible);
    void RemoveAllChoiceButtons();
    void AddChoiceButton(string buttonText, Action<Choice> onSelect, Choice choice);
}
