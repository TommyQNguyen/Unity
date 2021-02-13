using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    public Text PlayerHealthText;
    public Image PlayerHealthImage;
    public Health Health { get; private set; }

    public void Awake()
    {
        Health = GetComponent<Health>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // a voir pourquoi les points de vies ne s'affichent pas
        //PlayerHealthText.text = "x " + Health.PlayerHealthQuantity;
        PlayerHealthText.text = "Bonjour";
        PlayerHealthText.color = PlayerHealthText.color.WithAlpha(0.5f);
        PlayerHealthImage.color = PlayerHealthText.color.WithAlpha(0.5f);
    }
}
