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
        // Pour verifier la position si Mario est a gauche ou a droite, tirer du bon sens:

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log(GameManager.Instance.Mario.transform.position.x);
            Debug.Log(" Cannon position: " + gameObject.transform.position.x);
        }

        float marioCurrentPosition = GameManager.Instance.Mario.transform.position.x;
        float cannonCurrentPosition = gameObject.transform.position.x;

     

        if (!GameManager.Instance.IsInsideCamera(SpriteRenderer))
            return;

        CannonTimer = CannonTimer - Time.deltaTime;

        if (CannonTimer < 0)
        {
            if (marioCurrentPosition < cannonCurrentPosition)
            {
                SpawnBulletBill(LeftBulletSpawnPoint);
                CannonTimer = 4;
            }
            else if (marioCurrentPosition > cannonCurrentPosition)
            {
                SpawnBulletBill(RightBulletSpawnPoint);
                CannonTimer = 4;
            }
        }

    }

    void SpawnBulletBill(Transform BulletSpawnPoint)
    {
        float marioCurrentPosition = GameManager.Instance.Mario.transform.position.x;
        float cannonCurrentPosition = gameObject.transform.position.x;

        if (marioCurrentPosition < cannonCurrentPosition)
        {
            var bulletBillGameObject = GameManager.Instance.PrefabManager.PlatformerSpawn(PrefabManager.PlatformerGlobal.Bullet_Bill, BulletSpawnPoint);
            var bulletBill = GetComponent<BulletBill>();
        }
        else if (marioCurrentPosition > cannonCurrentPosition)
        {
            var bulletBillGameObject = GameManager.Instance.PrefabManager.PlatformerSpawn(PrefabManager.PlatformerGlobal.Bullet_Bill, BulletSpawnPoint);
            var bulletBill = bulletBillGameObject.GetComponent<BulletBill>();
            bulletBill.PlatformController.FacingController.Flip();
        }

        GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerSfx.Thwomp);
    }
}
