using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int killCount = 0;
    public int hpCount = 30;
    public int pillarCount = 8;

    public RawImage winImage;
    public RawImage loseImage;

    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI hpCountText;
    public TextMeshProUGUI baseHpCountText;

    void Start()
    {
        killCountText.text = "Kill: " + killCount + "/10";
        hpCountText.text = "Player HP: " + hpCount + "/30";
        baseHpCountText.text = "Base (Pillar) HP: " + pillarCount + "/8";
    }

    public void AddKillCount()
    {
        if (killCount < 10) 
        {
            killCount++;
            killCountText.text = "Kill: " + killCount + "/10";
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

    public void PillarDestroyed()
    {
        if (pillarCount > 0) 
        {
            pillarCount--;
            baseHpCountText.text = "Base (Pillar) HP: " + pillarCount + "/8";
        }

        if (pillarCount == 0) 
        {
            Debug.Log("Game Over");
            loseImage.gameObject.SetActive(true);
            winImage.gameObject.SetActive(false);
        }
    }
}
