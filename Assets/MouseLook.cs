using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Pour que le joueur regarde vers la direction de la souris
        var mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var lookAtAngle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(lookAtAngle, Vector3.forward);
    }
}
