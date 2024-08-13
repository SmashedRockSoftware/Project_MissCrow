using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlimeAnimation : MonoBehaviour
{
    [SerializeField] Vector3 punchScale = Vector3.one * 0.1f;
    [SerializeField] float punchDuration = 0.15f;
    [SerializeField] float shrinkDuration = 2f;

    [SerializeField] ParticleSystem[] particleSystems;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PokeSlime() {
        transform.DOPunchScale(punchScale, punchDuration).OnComplete(() => {
            transform.DOScale(Vector3.zero, shrinkDuration);
        });

        for (int i = 0; i < particleSystems.Length; i++) {
            //particleSystems[i].gameObject.SetActive(true);
            particleSystems[i].Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
