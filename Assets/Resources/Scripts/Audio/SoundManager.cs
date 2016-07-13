﻿using System.Collections;
using UnityEngine;

namespace Sliders
{
    public class SoundManager : MonoBehaviour
    {
        public AudioSource sfxSource;                   //Drag a reference to the audio source which will play the sound effects.
        public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
        public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.
        public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
        public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

        public AudioListener audioListener;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
                Destroy(gameObject);

            //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Game.onGameStateChange.AddListener(GameStateChanged);
        }

        private void GameStateChanged(Game.GameState gameState)
        {
            switch (gameState)
            {
                case Game.GameState.playing:
                    //play spawn sound
                    break;

                case Game.GameState.deathscreen:
                    //play death sound
                    break;

                case Game.GameState.finishscreen:
                    //play win sound
                    break;

                default:
                    break;
            }
        }

        //Used to play single sound clips.
        public void PlaySingle(AudioClip clip)
        {
            //Set the clip of our efxSource audio source to the clip passed in as a parameter.
            sfxSource.pitch = 1;
            sfxSource.clip = clip;

            //Play the clip.
            sfxSource.Play();
        }

        //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
        public void RandomizeSfx(params AudioClip[] clips)
        {
            //Generate a random number between 0 and the length of our array of clips passed in.
            int randomIndex = Random.Range(0, clips.Length);

            //Choose a random pitch to play back our clip at between our high and low pitch ranges.
            float randomPitch = Random.Range(lowPitchRange, highPitchRange);

            //Set the pitch of the audio source to the randomly chosen pitch.
            sfxSource.pitch = randomPitch;

            //Set the clip to the clip at our randomly chosen index.
            sfxSource.clip = clips[randomIndex];

            //Play the clip.
            sfxSource.Play();
        }
    }
}