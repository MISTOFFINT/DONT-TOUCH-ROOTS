using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambient : MonoBehaviour
{
    public AudioSource short_sound;
    public AudioClip[] short_sound_clips;
    public float next_short_s_MIN_delay = 20;
    public float next_short_s_MAX_delay = 50;
    public AudioSource[] long_sounds;
    public float new_long_s_play_time = 3;
    IEnumerator Start()
    {
        StartCoroutine("Shot_COR");
        int random_id = 0;
        int current_long_sound_id = 0;
        while(true)
        {
            random_id = Random.Range(0, long_sounds.Length);
            if(random_id == current_long_sound_id) random_id = random_id + 1;
            if(random_id == long_sounds.Length) random_id = 0;
            current_long_sound_id = random_id;
            long_sounds[random_id].Play();
            yield return new WaitForSeconds(long_sounds[random_id].clip.length - new_long_s_play_time);
        }
    }
    IEnumerator Shot_COR()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(next_short_s_MIN_delay, next_short_s_MAX_delay));
            short_sound.clip = short_sound_clips[Random.Range(0, short_sound_clips.Length)];
            short_sound.Play();
        }
    }


}
