using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    public Text PlayerHealthText;
    public Image PlayerHealthImage;

    // Update is called once per frame
    void Update()
    {
        PlayerHealthText.text = "Bonjour!";
        PlayerHealthText.color = PlayerHealthText.color.WithAlpha(0.5f);
        PlayerHealthImage.color = PlayerHealthText.color.WithAlpha(0.5f);
    }
}
