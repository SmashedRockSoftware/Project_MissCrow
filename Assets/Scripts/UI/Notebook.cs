using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notebook : MonoBehaviour
{
    [SerializeField] GameObject notebookPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterNotebook() {
        notebookPanel.SetActive(true);
        GameManager.Instance.NotebookGame();
    }

    public void ExitNotebook() {
        notebookPanel.SetActive(false);
        GameManager.Instance.UnNotebookGame();
    }
}
