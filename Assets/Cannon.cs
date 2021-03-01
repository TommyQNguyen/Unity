using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform LeftBulletSpawnPoint;
    public Transform RightBulletSpawnPoint;
    void Awake()
    {
        InvokeRepeating("SpawnBulletBill", 4, 4);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!GameManager.Instance.IsInsideCamera(SpriteRenderer))
        //    return;
    }

    void SpawnBulletBill()
    {
        GameManager.Instance.PrefabManager.PlatformerSpawn(PrefabManager.PlatformerGlobal.Goomba, LeftBulletSpawnPoint.position);
        GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerSfx.Thwomp);
    }
}
