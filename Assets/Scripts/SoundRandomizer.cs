using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRandomizer : MonoBehaviour
{
    private float delayTime;
    public AudioClip[] phrases;
    private AudioSource src;
    private void Start()
    {
        src = this.GetComponent<AudioSource>();
        StartCoroutine(SpeakRandomPhrase());
    }

    IEnumerator SpeakRandomPhrase()
    {
        while (true)
        {
            delayTime = Random.Range(2f, 3f);
            yield return new WaitForSeconds(delayTime);

            int randomIndex = Random.Range(0, phrases.Length);

            src.PlayOneShot(phrases[randomIndex]);
        }
    }
}
