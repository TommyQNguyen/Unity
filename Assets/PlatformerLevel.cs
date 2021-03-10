using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerLevel : MonoBehaviour
{
    public SoundManager.PlatformerMusic Music = SoundManager.PlatformerMusic.Music;

    private void Awake()
    {
        GameManager.Instance.SoundManager.PlatformerPlay(Music);
    }
}
