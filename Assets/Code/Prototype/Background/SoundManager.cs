using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        //Outlets
        AudioSource audioSource;
        public AudioClip missSound;
        public AudioClip hitSound;

        void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void PlaySoundHit()
        {
            audioSource.PlayOneShot(hitSound);
        }
        public void PlaySoundMiss()
        {
            audioSource.PlayOneShot(missSound);
        }
    }