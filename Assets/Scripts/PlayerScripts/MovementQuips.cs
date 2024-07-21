using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementQuips : MonoBehaviour
{
    public const string moveQuip = "AttemptToPlayMovmentQuip";

    [SerializeField] List<string> quips = new List<string>();
    [SerializeField] int quipFrequency = 25;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        PlayerMovement.OnGoto += AttemptToPlayMovmentQuip;
    }

    private void OnDisable() {
        PlayerMovement.OnGoto -= AttemptToPlayMovmentQuip;
    }

    public void AttemptToPlayMovmentQuip() {
        if(!DialogueUI.Instance.IsGrannyPanelInUse() && Random.Range(0, 100) < quipFrequency)
            DialogueUI.Instance.DisplayGrannyText(quips[Random.Range(0, quips.Count-1)]);
    }
}
