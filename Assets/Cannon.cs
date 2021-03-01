using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform LeftBulletSpawnPoint;
    public Transform RightBulletSpawnPoint;
    public float CannonTimer;


    public SpriteRenderer SpriteRenderer { get; private set; }

    void Awake()
    {
        //InvokeRepeating("SpawnBulletBill", CannonTimer, CannonTimer);
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsInsideCamera(SpriteRenderer))
            return;

        CannonTimer = CannonTimer - Time.deltaTime;

        if (CannonTimer < 0)
        {
            SpawnBulletBill();
            CannonTimer = 4;
        }

    }

    void SpawnBulletBill()
    {
        GameManager.Instance.PrefabManager.PlatformerSpawn(PrefabManager.PlatformerGlobal.Bullet_Bill, LeftBulletSpawnPoint.position);
        GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerSfx.Thwomp);
    }
}
