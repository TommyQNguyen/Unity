using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    private Player Player;
    public Text PlayerHealthText;
    public Image PlayerHealthImage;
    public Text PlayerBombText;
    public Image PlayerBombImage;
    public Text PlayerScoreText;
    public Text GameOverText;

    private void Start()
    {
        Player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Health.PlayerHealthQuantity < 1)
        {
            GameOverText.text = "Game Over";
            //Debug.Log("Game Over!");
        }
        
        PlayerHealthText.text = Player.Health.PlayerHealthQuantity.ToString();
        //PlayerHealthText.text = "Bonjour";
        //PlayerHealthText.color = PlayerHealthText.color.WithAlpha(0.5f);
        //PlayerHealthImage.color = PlayerHealthText.color.WithAlpha(0.5f);

        PlayerBombText.text = "x " + Player.Items.BombQuantity;
        //PlayerBombText.color = PlayerBombText.color.WithAlpha(0.7f);
        //PlayerBombImage.color = PlayerBombImage.color.WithAlpha(0.7f);
        PlayerScoreText.text = Player.Score.ScoreNumber.ToString();
    }
}
