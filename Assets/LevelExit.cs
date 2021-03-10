using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public LevelManager.Level Level;
    public string LevelEntranceId;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var mario = collision.GetComponent<Mario>();
        if (mario)
        {
            GameManager.Instance.LevelManager.GoToLevel(Level, LevelEntranceId);
        }
    }
}
