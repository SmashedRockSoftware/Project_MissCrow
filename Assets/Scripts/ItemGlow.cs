using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGlow : MonoBehaviour
{
    [SerializeField] Renderer rend;
    [SerializeField] Material outlineMaterial;

    KeyCode revelInteractableKey = KeyCode.H;

    // Start is called before the first frame update
    void Start()
    {
        rend = rend.GetComponent<Renderer>();
    }



    void OnMouseEnter() {
        MakeObjectGlow();
    }

    private void MakeObjectGlow() {
        Material[] materials = {
            rend.material,
            outlineMaterial,
        };

        rend.materials = materials;
    }

    void OnMouseOver() {

    }


    void OnMouseExit() {
        RemoveGlow();
    }

    private void RemoveGlow() {
        Material[] materials = {
            rend.material,
        };

        rend.materials = materials;
    }

    void Update() {
        if (Input.GetKeyDown(revelInteractableKey)) {
            MakeObjectGlow();
        }

        if (Input.GetKeyUp(revelInteractableKey)) {
            RemoveGlow();
        }
    }
}
