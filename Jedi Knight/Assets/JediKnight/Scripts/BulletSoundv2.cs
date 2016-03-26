using UnityEngine;
using System.Collections;

public class BulletSoundv2 : MonoBehaviour {
	void Start () {
		AudioSource sound = gameObject.GetComponent<AudioSource>();
		sound.Play();
	}
}
