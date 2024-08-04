using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemGlow : MonoBehaviour
{
    [SerializeField] Renderer rend;
    Material outlineMaterial;

    KeyCode revelInteractableKey = KeyCode.H;

    Material defaultOutline;
    Material combinedOutline;

    // Start is called before the first frame update
    void Start() {
        rend = rend.GetComponent<Renderer>();
        if(rend == null)
            Debug.Log("Renderer has not been assigned, we cant glow! " + rend.gameObject.name);

        defaultOutline = Resources.Load<Material>("Material/defaultOutline");
        combinedOutline = Resources.Load<Material>("Material/combinedOutline");

        MarkAsRegular();
    }



    void OnMouseEnter() {
        MakeObjectGlow();
    }

    private void MakeObjectGlow() {
        if (rend == null) return;

        List<Material> materials = new List<Material>();
        materials.AddRange(rend.materials.ToList());

        bool alreadyHightlighted = false;
        alreadyHightlighted = CheckIfAlreadyOutlined(materials, alreadyHightlighted);

        if (!alreadyHightlighted)
            materials.Add(outlineMaterial);

        rend.materials = materials.ToArray();

        bool CheckIfAlreadyOutlined(List<Material> materials, bool alreadyHightlighted) {
            for (int i = 0; i < materials.Count; i++) {
                if (materials[i].name.Contains(outlineMaterial.name)) {
                    alreadyHightlighted = true;
                }
            }

            return alreadyHightlighted;
        }
    }

    public void MarkAsCombined() {
        outlineMaterial = combinedOutline;
    }
    public void MarkAsRegular() {
        outlineMaterial = defaultOutline;
    }

    void OnMouseOver() {

    }


    void OnMouseExit() {
        RemoveGlow();
    }

    private void RemoveGlow() {
        if (rend == null) return;

        List<Material> materials = new List<Material>();
        materials.AddRange(rend.materials.ToList());

        RemoveOutlinedMaterials(materials);

        rend.materials = materials.ToArray();

        void RemoveOutlinedMaterials(List<Material> materials) {
            for (int i = 0; i < materials.Count; i++) {
                if (materials[i].name.Contains(defaultOutline.name) || materials[i].name.Contains(combinedOutline.name)) {
                    materials.RemoveAt(i);
                }
            }
        }
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
