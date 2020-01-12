using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class MasterAudioSummoner : MonoBehaviour {
    public static MasterAudioSummoner instance;
	

    public List<AudioClip> EffectSounds;
    public List<AudioClip> MenuSounds;
    public List<AudioClip> WeaponSounds;
    public List<AudioClip> ExplosionSounds;
	public List<AudioClip> ImpactSounds;
	public List<AudioClip> AnnouncerSounds;
	public List<AudioClip> AmbienceSounds;


	public float targetVolume = 1f;

    bool playingClip;
    // Use this for initialization
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
		GetAllSounds(EffectSounds, "Effects");
		GetAllSounds(MenuSounds, "Menu");
		GetAllSounds(WeaponSounds, "Weapons");
		GetAllSounds(ExplosionSounds, "Explosions");
		GetAllSounds(AnnouncerSounds, "Announcer/HON");
		GetAllSounds(ImpactSounds, "ImpactSounds");
		GetAllSounds(AmbienceSounds, "Ambience");
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}
	void GetAllSounds(List<AudioClip> bank, string s) {
		bank.Clear();
		var clips = Resources.LoadAll("Sounds/" + s, typeof(AudioClip)).Cast<AudioClip>().ToArray();
		foreach (var t in clips) {
			bank.Add(t);
		}
	}

	AudioClip FindFile(string fileName, List<AudioClip> clipBank)
    {
        if (fileName == "" || fileName == null) return null;
        if (clipBank.Count == 0)
        {
            print("Invalid clip bank - count is 0!");
            return null;
        }
        for (int i = 0; i < clipBank.Count; i++)
        {
			if (clipBank[i] == null) {
				i++;
			}
            if (fileName == clipBank[i].name)
            {
                return clipBank[i];
            }
        }
        print("Could not find audio by name; " + fileName);
        return null;
    }

    public void PlayAudio(List<AudioClip> clipBank, string audioClipName, float volumeLevel, float dopplerRange, Vector3 position, Transform parent)
    {
        AudioClip clip = FindFile(audioClipName, clipBank);
        if (clip == null)
        {
            print("Clip is null - unable to play file (attempted to play;" + audioClipName + " ).");
            return;
        }
		GameObject go = new GameObject(audioClipName + ("AudioClip"));
		go.transform.position = position;
		go.transform.rotation = Quaternion.identity;
		//GameObject go = Instantiate(new GameObject(audioClipName + ("AudioClip")), position, Quaternion.identity) as GameObject;
		if (parent != null) go.transform.parent = parent;
        AudioSource source = go.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.volume = volumeLevel * targetVolume;
        source.clip = clip;
        
        DestroyAfterTime script = go.AddComponent<DestroyAfterTime>();
        script.randomDopplerRange = dopplerRange;
        source.Play();
		if (clipBank != MenuSounds && clipBank != AnnouncerSounds) {
			LerpSoundByDistance lerpSound = go.AddComponent<LerpSoundByDistance>();
		}
    }
	//can be used to play audio with delay
	/*
    IEnumerator PlayClip(float delay, string audioClipName, float volumeLevel, float dopplerRange, Vector3 position)
    {
        yield return new WaitForSeconds(delay);
        AudioClip clip = FindFile(audioClipName, bettySounds);
        if (clip == null)
        {
            print("Clip is null - unable to play file (attempted to play;" + audioClipName + " ).");
            yield break;
        }
        GameObject go = Instantiate(AudioObject, position, Quaternion.identity) as GameObject;
        AudioSource source = go.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.volume = volumeLevel * targetVolume;
        source.clip = clip;
        DestroyAfterTime script = go.AddComponent<DestroyAfterTime>();
        script.randomDopplerRange = dopplerRange;
        source.Play();
        playingClip = false;
    }
	*/
}
