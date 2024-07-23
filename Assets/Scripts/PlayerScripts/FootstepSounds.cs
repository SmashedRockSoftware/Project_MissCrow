using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    Vector3 lastPostion;
    [SerializeField] float distanceToStep = 4f;

    [Space]
    [SerializeField] AudioSource footstepSource;
    [SerializeField] float minPitch = 0.8f, maxPitch = 1.2f, startingPitch = 1f;
    [SerializeField] List<AudioClip> clips = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        lastPostion = transform.position;
    }

    private void PlayFootstep() {
        if(footstepSource.isPlaying) { return; }

        footstepSource.clip = clips[Random.Range(0, clips.Count-1)];
        footstepSource.pitch = startingPitch + Random.Range(minPitch, maxPitch);
        lastPostion = transform.position;

        footstepSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(lastPostion, transform.position) > distanceToStep) {
            PlayFootstep();
        }
    }
}
