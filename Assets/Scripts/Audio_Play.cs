using UnityEngine;

public class Audio_Play : MonoBehaviour
{
    AudioSource aud;
    //Start is called before the first fram update

    void Start()
    {
        aud = GetComponent<AudioSource>();


    }

    //update is called once per frame
    void Update()
    {

    }

    public void play_sound()
    {
        aud.Play();
    }
}
