using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAssistant : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public static AudioAssistant instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start() {
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetItemName(ItemScriptableObject itemData) {
        return itemData.ToString().Replace(" (ItemScriptableObject)", "");
    }

    public string GetComboName(Combination comboData) {
        return comboData.ToString().Replace(" (CombinationScriptableObject)", "");
    }

    public string GetComboName(BadCombo badComboData) {
        return badComboData.ToString().Replace(" (BadComboScriptableObject)", "");
    }

    public void PlayResourceSoundAtPoint(ItemScriptableObject itemData, string soundName, Vector3 position) {
        var name = GetItemName(itemData);

        string str = "Sounds/" + name + "/" + soundName;
        var audioClip = Resources.Load<AudioClip>(str);

        if (audioClip == null) {
            Debug.Log("1No sound for " + name);
            audioClip = Resources.Load<AudioClip>("Sounds/Generic/" + soundName);
        }
        else { Debug.Log("sound for " + name); }

        var audiosrc = Instantiate(audioSource, position, Quaternion.identity, transform);
        audiosrc.clip = audioClip;
        audiosrc.Play();

        Destroy(audiosrc.gameObject, audioClip.length);
    }

    public void PlayResourceSoundAtPoint(Combination comboData, string soundName, Vector3 position) {
        var name = GetComboName(comboData).Replace(" (Combination)", "");

        string str = "Sounds/Combo/" + name;
        var audioClip = Resources.Load<AudioClip>(str);

        if (audioClip == null) {
            Debug.Log("No sound for " + name);
            audioClip = Resources.Load<AudioClip>("Sounds/Generic/" + soundName);
        }
        else { Debug.Log("sound for " + name); }

        var audiosrc = Instantiate(audioSource, position, Quaternion.identity, transform);
        audiosrc.clip = audioClip;
        audiosrc.Play();

        Destroy(audiosrc.gameObject, audioClip.length);
    }

    public void PlayResourceSoundAtPoint(BadCombo badComboData, string soundName, Vector3 position) {

        var audioClip = Resources.Load<AudioClip>("Sounds/Generic/" + soundName);

        if (badComboData != null) {
            var name = GetComboName(badComboData);

            string str = "Sounds/BadCombo/" + name;
            audioClip = Resources.Load<AudioClip>(str);
        }

        if (audioClip == null)
            audioClip = Resources.Load<AudioClip>("Sounds/Generic/" + soundName);

        var audiosrc = Instantiate(audioSource, position, Quaternion.identity, transform);
        audiosrc.clip = audioClip;
        audiosrc.Play();

        Destroy(audiosrc.gameObject, audioClip.length);
    }
}
