using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public PlatformController PlatformController { get; private set; }
    public Animator Animator { get; private set; }

    void Awake()
    {
        PlatformController = GetComponent<PlatformController>();
        PlatformController.OnWall += OnWall;
        PlatformController.OnMoveStart += OnMoveStart;

        Animator = GetComponent<Animator>();
    }

    private void OnMoveStart(PlatformController platformController)
    {
        Animator.Play("Fireball");
    }

    private void OnWall(PlatformController platformController)
    {
        GameManager.Instance.PrefabManager.PlatformerSpawn(PrefabManager.PlatformerGlobal.Smoke, transform.position);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        PlatformController.InputMove = PlatformController.FacingController.Direction;
        PlatformController.InputJump = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var mario = collision.GetComponent<Mario>();
        var health = collision.GetComponentInParent<Health>();

        if (health && health.CanBeDamaged && !mario)
        {
            health.Value -= 1;
            Destroy(gameObject);
            GameManager.Instance.PrefabManager.PlatformerSpawn(PrefabManager.PlatformerGlobal.Smoke, transform.position);
            GameManager.Instance.SoundManager.PlatformerPlay(SoundManager.PlatformerSfx.Kick);
        }
    }
}
