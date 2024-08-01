using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] float randomMin = 10.01f, randomMax = 20.01f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        timer = Random.Range(randomMin, randomMax);
    }

    void PlaySound() {
        timer = Random.Range(randomMin, randomMax);

        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.currentGameState == GameState.InGame) {
            timer -= Time.deltaTime;

            if(timer <= 0 && !audioSource.isPlaying) {
                PlaySound();
            }

        }
    }
}
