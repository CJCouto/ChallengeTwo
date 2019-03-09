using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioClip firstClip;
    public AudioClip secondClip;
    public int count;
    public bool winSound = false;
    AudioSource sfx;

    public GameObject player;

    // Use this for initialization
    void Start () {
        sfx = GetComponent<AudioSource>();
        sfx.clip = firstClip;
        sfx.Play();
        count = player.GetComponent<PlayerController>().count;
    }
	
	// Update is called once per frame
	void Update () {
        count = player.GetComponent<PlayerController>().count;
        if (count >= 8 && winSound == false) {
            sfx.loop = false;
            sfx.clip = secondClip;
            sfx.Play();
            winSound = true;
        }
	}
}
