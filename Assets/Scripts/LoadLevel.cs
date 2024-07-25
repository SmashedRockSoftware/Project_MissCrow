using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    float fadeTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeThenLoadLevel(int index) {
        FadingCanvas.instance.FadeOut();
        StartCoroutine(afterTimeCallLoad(index));
    }

    IEnumerator afterTimeCallLoad(int index) {
        yield return new WaitForSeconds(fadeTime);
        LoadALevel(index);
    }

    public void LoadALevel(int index) {
        SceneManager.LoadScene(index);
    }
}
