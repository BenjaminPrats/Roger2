using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer _audioMixer;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(MuteSoundFor(10.0f));
    }

    IEnumerator MuteSoundFor(float seconds)
    {
        float volume = 1.0f;

        if (_audioMixer.GetFloat("VolumeFx", out volume))
            Debug.Log("Worked");
        else
            Debug.Log("Did not");
        // Hide sound during the initiliazition of trap
        _audioMixer.SetFloat("VolumeFx", -80.0f);
        Debug.Log("Start waiting");
        
        yield return new WaitForSeconds(seconds);

        Debug.Log("End waiting");
        // put back the right volume
        _audioMixer.SetFloat("VolumeFx", volume);
        
    }
}
