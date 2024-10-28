using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int killCount = 0;
    public int hpCount = 30;

    public RawImage winImage;
    public RawImage loseImage;

    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI hpCountText;

    void Start()
    {
        killCountText.text = "Kill: " + killCount;
        hpCountText.text = "HP: " + hpCount;
    }

    public void AddKillCount()
    {
        if (killCount < 10) 
        {
            killCount++;
            killCountText.text = "Kill: " + killCount;
        }

        if (killCount == 10)
        {
            Debug.Log("You Win");
            winImage.gameObject.SetActive(true);
            loseImage.gameObject.SetActive(false);
        }
    }

    public void SubtractHpCount()
    {
        if (hpCount >= 0) 
        {
            hpCount--;
            hpCountText.text = "HP: " + hpCount;
        }

        if (hpCount == 0) 
        {
            Debug.Log("Game Over");
            loseImage.gameObject.SetActive(true);
            winImage.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Player hit by enemy bullet");
            SubtractHpCount();
        }
    }
}
