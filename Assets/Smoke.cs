using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public Vector2 SmokeAnimationSpeed;
    public Animator Animator { get; private set; }

    //public float DestroyTimer = 1;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator.Play("Smoke");
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy(gameObject, DestroyTimer);

    }
}
