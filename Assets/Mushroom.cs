using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public PlatformController PlatformController { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        PlatformController = GetComponent<PlatformController>();
        PlatformController.OnWall += OnWall;
    }

    private void OnWall(PlatformController platformController)
    {
        PlatformController.FacingController.Flip();
    }

    // Update is called once per frame
    void Update()
    {
        PlatformController.InputMove = PlatformController.FacingController.Direction;
    }

    
}
