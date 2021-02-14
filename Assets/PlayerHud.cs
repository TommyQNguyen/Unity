using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    private GameObject Player;
    public Text PlayerHealthText;
    public Image PlayerHealthImage;
    public Health Health { get; private set; }

    public void Awake()
    {
        Health = GetComponent<Health>(); 
    }

    private void Start()
    {
        //Player player = FindObjectOfType<Player>();
        //player.gameObject
        Player = FindObjectOfType<Player>().gameObject;

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
