using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered");

        var mario = collision.GetComponent<Mario>();

        if (mario)
        {
            Debug.Log("SHELL triggered by Mario!");
            
        }
    }

   
}
