using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCutscne : MonoBehaviour
{
    CrowAnimation crowAnimation;
    bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        crowAnimation = FindAnyObjectByType<CrowAnimation>();

        if (crowAnimation == null) { Debug.LogError("Can perform opening cutscene no crow animation script found :("); return; }

        GameManager.Instance.CutsceneGame();
        crowAnimation.WakeUpAnimation();
        isPlaying = true;
    }

    public void EndOpening() {
        GameManager.Instance.UnCutsceneGame();
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
