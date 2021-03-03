using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBill : MonoBehaviour
{
    public PlatformController PlatformController { get; private set; }
    public Health Health { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        PlatformController = GetComponent<PlatformController>();
        PlatformController.OnWall += OnWall;

        Health = GetComponent<Health>();
        Health.OnDeath += OnDeath;
    }

    private void OnWall(PlatformController platformController)
    {
        Destroy(gameObject);
    }

    private void OnDeath(Health health)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        PlatformController.InputMove = PlatformController.FacingController.Direction;

    }
}
