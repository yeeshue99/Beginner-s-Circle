using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public AudioSource withoutTail;
    public AudioSource withTail;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayMusic());
    }

    // Update is called once per frame
    IEnumerator PlayMusic()
    {
        withoutTail.Play();
        yield return new WaitForFixedUpdate();
        withTail.Play();
    }
}
