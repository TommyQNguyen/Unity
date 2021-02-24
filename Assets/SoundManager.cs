using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum ShooterMusic
    {
        Music,

        Count
    }

    public enum PlatformerMusic
    {
        Music,

        Count
    }

    public enum ShooterSfx
    {
        Explosion,
        Hit,
        Hurt,
        Pickup,
        Pistol,
        Shotgun,
        Spawn,

        Count
    };

    public enum PlatformerSfx
    {
        Jump,

        Count
    };



    public AudioClip[] MusicAudioClips;
    public AudioClip[] SfxAudioClips;

    public AudioSource MusicAudioSource { get; private set; }


    public void Awake()
    {
        // https://docs.unity3d.com/ScriptReference/Resources.html
        MusicAudioClips = Resources.LoadAll<AudioClip>("platformer/audio/music");
        Debug.Assert((int)PlatformerMusic.Count == MusicAudioClips.Length, "SoundManager : Music enum length (" + (int)PlatformerMusic.Count + ") does not match Resources folder (" + MusicAudioClips.Length + ")");

        SfxAudioClips = Resources.LoadAll<AudioClip>("platformer/audio/sfx");
        Debug.Assert((int)PlatformerSfx.Count == SfxAudioClips.Length, "SoundManager : Sfx enum length " + (int)PlatformerSfx.Count + ") does not match Resources folder (" + SfxAudioClips.Length + ")");

        // https://docs.unity3d.com/ScriptReference/GameObject.AddComponent.html
        MusicAudioSource = gameObject.AddComponent<AudioSource>();
        MusicAudioSource.loop = true;
        MusicAudioSource.volume = 0.08f;
    }

    public void Play(ShooterMusic music)
    {
        MusicAudioSource.clip = MusicAudioClips[(int)music];
        MusicAudioSource.Play();
    }

    public void Play(ShooterSfx sfx)
    {
        AudioSource.PlayClipAtPoint(SfxAudioClips[(int)sfx], transform.position);
    }

    public void PlatformerPlay(PlatformerMusic music)
    {
        MusicAudioSource.clip = MusicAudioClips[(int)music];
        MusicAudioSource.Play();
    }

    public void PlatformerPlay(PlatformerSfx sfx)
    {
        AudioSource.PlayClipAtPoint(SfxAudioClips[(int)sfx], transform.position);
    }
}
