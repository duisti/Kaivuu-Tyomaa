using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(AudioSource))]

public class MusicManager : MonoBehaviour {
    public static MusicManager instance;

    AudioSource source;
    public List<AudioClip> MusicTracks;

    bool fadingIn;
    bool fadingOut;
    float fadeTimer = 1f;

    public float audioVolumeCap = 0.5f;
    // Use this for initialization
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
        source = GetComponent<AudioSource>();
        source.volume = audioVolumeCap;

		GetAllMusic(MusicTracks);
    }

	void GetAllMusic(List<AudioClip> bank) {
		var clips = Resources.LoadAll("Sounds/Music", typeof(AudioClip)).Cast<AudioClip>().ToArray();
		foreach (var t in clips) {
			bank.Add(t);
		}
	}

    void Start()
    {

    }

    // Update is called once per frame
    void Update () {

        if (fadingIn)
        {
            if (source.volume <= 0) {
                source.volume = 0f;
                fadingIn = false;
                return;
            }

            source.volume -= fadeTimer * Time.unscaledDeltaTime;
        }
        if (fadingOut)
        {
            if (source.volume >= audioVolumeCap)
            {
                source.volume = audioVolumeCap;
                fadingOut = false;
                return;
            }
            source.volume += fadeTimer * Time.unscaledDeltaTime;
        }

    }

    AudioClip FindFile(string fileName)
    {
        if (fileName == "" || fileName == null) return null;
        if (MusicTracks.Count == 0)
        {
            print("Invalid clip bank - count is 0!");
            return null;
        }
        for (int i = 0; i < MusicTracks.Count; i++)
        {
            if (fileName == MusicTracks[i].name)
            {
                return MusicTracks[i];
            }
        }
        print("Could not find audio by name; " + fileName);
        return null;
    }

    public void Stop(bool immediate)
    {
        if (immediate)
        {
            source.Stop();
        } else
        {
            StartCoroutine(ChangeMusic(null, audioVolumeCap));
        }
    }

    public void PlayFile(string trackName)
    {
        if (fadingIn || fadingOut) return;
        fadingIn = true;
        fadingOut = false;
		float vol = 0.5f;
		if (trackName == "BossMusicLoop" ||
			trackName == "finalboss" ||
			trackName == "Drok" ||
			trackName == "TranceLVania"
			) vol = 1f;
		audioVolumeCap = vol;
        StartCoroutine(ChangeMusic(FindFile(trackName), audioVolumeCap));

    }

    IEnumerator ChangeMusic(AudioClip clip, float volume)
    {
        while(fadingIn)
        {
            yield return null;
        }
        fadingIn = false;
        if (clip == null) StopAllCoroutines();
        source.Stop();
        source.clip = clip;
        source.loop = true;
        source.Play();
        fadingOut = true;
        while (fadingOut)
        {
            yield return null;
        }
        //do we even want to do anything here?

    }
}
