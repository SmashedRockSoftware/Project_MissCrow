using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAssistant : MonoBehaviour
{
    AudioSource audioSource;

    public static AudioAssistant instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetItemName(ItemScriptableObject itemData) {
        return itemData.ToString().Replace(" (ItemScriptableObject)", "");
    }

    public void PlayResourceSoundAtPoint(ItemScriptableObject itemData, string soundName, Vector3 position) {
        var name = GetItemName(itemData);

        string str = "Sounds/" + name + "/" + soundName;
        var audioClip = Resources.Load<AudioClip>(str);

        if (audioClip == null)
            audioClip = Resources.Load<AudioClip>("Sounds/Generic/pickup");

        AudioSource.PlayClipAtPoint(audioClip, position);
    }
}
