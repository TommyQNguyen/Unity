using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMovement : MonoBehaviour
{

    public float Speed = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        // Etape pour empecher le joueur d'aller en diagonale trop vite
        var direction = new Vector3(horizontal, vertical, 0).normalized;
        // Si le joueur avance en diagonale avec un vecteur de plus que 1,
        // le ramener a une vitesse normale
        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }


        transform.position += new Vector3(horizontal * Speed * Time.deltaTime, vertical * Speed * Time.deltaTime, 0);

    }
}
