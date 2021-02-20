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

        var player = GameManager.Instance.Player;

        player.Health.OnChanged += OnHealthChanged;
        //player.Bombs.OnChanged += OnBombsChanged;
        //player.Score.OnChanged += OnScoreChanged;

        OnHealthChanged(player.Health);
        //OnBombsChanged(player.Bombs);
        //OnScoreChanged(player.Score);
    }

    private void OnHealthChanged(Health health)
    {
        PlayerHealthText.text = health.Value.ToString();
    }

    //private void OnScoreChanged(Score score)
    //{
    //    PlayerScoreText.text = score.Value.ToString();
    //}

    //private void OnBombsChanged(Bombs bombs)
    //{
    //    PlayerBombsText.text = "x " + bombs.Value;
    //}

    // Update is called once per frame
    void Update()
    {
        if (Player.Health.Value < 1)
        {
            GameOverText.text = "Game Over";
            //Debug.Log("Game Over!");
        }

        //    PlayerHealthText.text = Player.Health.Value.ToString();
        //    //PlayerHealthText.text = "Bonjour";
        //    //PlayerHealthText.color = PlayerHealthText.color.WithAlpha(0.5f);
        //    //PlayerHealthImage.color = PlayerHealthText.color.WithAlpha(0.5f);

        PlayerBombText.text = "x " + Player.Items.BombQuantity;
        //PlayerBombText.color = PlayerBombText.color.WithAlpha(0.7f);
        //PlayerBombImage.color = PlayerBombImage.color.WithAlpha(0.7f);
        PlayerScoreText.text = Player.Score.ScoreNumber.ToString();
    }
}
