using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlimeTracker : MonoBehaviour
{
    bool isPokedSlime1 = false;
    bool isPokedSlime1001 = false;
    bool isPokedSlime1002 = false;
    bool isPokedSlime1003 = false;

    bool isPipesFixed = false;

    [SerializeField] UnityEvent OnFixPipes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPokedSlime1 && isPokedSlime1001 && isPokedSlime1002 && isPokedSlime1003 && !isPipesFixed) {
            FixPipes();
        }
    }

    public void FixPipes (){
        isPipesFixed = true;
        OnFixPipes.Invoke();
    }

    public void PokeSlime1() {
        isPokedSlime1 = true;
    }

    public void PokeSlime1001() {
        isPokedSlime1001 = true;
    }

    public void PokeSlime1002() {
        isPokedSlime1002 = true;
    }

    public void PokeSlime1003() {
        isPokedSlime1003 = true;
    }
}
