using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestComponent : MonoBehaviour

{
    // Awake is called before the first frame update
    void Awake()
    {
        Debug.Log(gameObject.name + " Awake");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name + " Start");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.name + " Update");
    }
}
