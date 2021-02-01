using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float Speed;

    private float _secret;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Quaternion
        // Euler angles
        // delta = 0.02
        transform.rotation *= Quaternion.Euler(0, Speed * Time.deltaTime, 0);

    
    }
}
