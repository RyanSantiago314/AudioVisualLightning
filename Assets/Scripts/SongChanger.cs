using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongChanger : MonoBehaviour
{
    public AudioClip track0;
    public AudioClip track1;
    public AudioClip track2;
    public AudioClip track3;

    AudioClip[] lightningSong = new AudioClip[4];
    AudioSource trackPlayer;

    int currentTrack = 1;

    // Start is called before the first frame update
    void Start()
    {
        lightningSong[0] = track0;
        lightningSong[1] = track1;
        lightningSong[2] = track2;
        lightningSong[3] = track3;

        trackPlayer = GetComponent<AudioSource>();
        trackPlayer.clip = lightningSong[currentTrack];
        trackPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextTrack()
    {
        if (currentTrack == 3)
            currentTrack = 0;
        else
            currentTrack++;
        trackPlayer.clip = lightningSong[currentTrack];
        trackPlayer.Play();
    }
    public void PrevTrack()
    {
        if (currentTrack == 0)
            currentTrack = 3;
        else
            currentTrack--;
        trackPlayer.clip = lightningSong[currentTrack];
        trackPlayer.Play();
    }
}
