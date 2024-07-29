using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrannySounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] float minPitch = 0.8f, maxPitch = 1.2f, startingPitch = 1f;
    [SerializeField] List<AudioClip> inspectClips = new List<AudioClip>();
    [SerializeField] List<AudioClip> goingClips = new List<AudioClip>();

    [SerializeField] int inspectLinesAmount = 50;
    [SerializeField] int goingLinesAmount = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoingToLocation() {
        if (audioSource.isPlaying) return;

        var rand = Random.Range(0, 100);
        if (rand > goingLinesAmount) return;

        if (goingClips.Count > 0) { audioSource.clip = goingClips[Random.Range(0, goingClips.Count - 1)]; }
        audioSource.pitch = startingPitch + Random.Range(minPitch, maxPitch);
        audioSource.Play();
    }

    public void InpectingAnItem() {
        if (audioSource.isPlaying) return;

        var rand = Random.Range(0, 100);
        if (rand > inspectLinesAmount) return;

        if(inspectClips.Count > 0) { audioSource.clip = inspectClips[Random.Range(0, inspectClips.Count - 1)]; }
        audioSource.pitch = startingPitch + Random.Range(minPitch, maxPitch);
        audioSource.Play();
    }
}
