using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityEventHandler), typeof(AudioSource))]
public class EntityAudioHandler : MonoBehaviour {

	EntityEventHandler entityEventHandler;
	AudioSource audioSource;

	[SerializeField] SoundData hitSound;
	[SerializeField] SoundData deathSound;

	void Awake() {
		entityEventHandler = GetComponent<EntityEventHandler>();
		audioSource = GetComponent<AudioSource>();
		
		entityEventHandler.OnHit += () => {
			AudioClip hitClip = Synthesizer.instance.GenerateSignal(hitSound);
			audioSource.PlayOneShot(hitClip);
		};

        entityEventHandler.OnDeath += () => {

		};
	}

    void Start() {
    }

    void Update() {
        
    }
}
