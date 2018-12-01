using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

    public AudioClip NightSounds;

    public AudioSource MusicSource;

	// Use this for initialization
	void Start () {

        MusicSource.clip = NightSounds;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
