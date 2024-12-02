using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int killCount = 0;
    public int hpCount = 30;
    public int pillarCount = 8;
    public Pillar pillarTemp;

    private int baseTotalHp = 0;

    public RawImage winImage;
    public RawImage loseImage;

    // public TextMeshProUGUI killCountText;
    // public TextMeshProUGUI hpCountText;
    // public TextMeshProUGUI baseHpCountText;

    public Slider hpSlider;
    public Slider baseHpSlider;

    public List<RawImage> killIndicators; // RawImage 10개를 담을 리스트
    public Texture newTexture; // 변경할 텍스처

    void Start()
    {
        // killCountText.text = "Kill: " + killCount + "/10";
        // hpCountText.text = "Player HP: " + hpCount + "/30";
        // baseHpCountText.text = "Base (Pillar) HP: " + pillarCount + "/8";

        if (hpSlider != null)
        {
            hpSlider.maxValue = hpCount; // 최대 HP 설정
            hpSlider.value = hpCount; // 현재 HP 설정
            UpdateHpSlider();
        }

        if (baseHpSlider != null)
        {
            int pillarHp = pillarTemp.maxTriggers - pillarTemp.triggerCount;
            baseHpSlider.maxValue = pillarCount * pillarHp; // 최대 HP 설정
            baseHpSlider.value = pillarCount * pillarHp; // 현재 HP 설정
        }

        baseTotalHp = pillarCount * (pillarTemp.maxTriggers - pillarTemp.triggerCount);
    }

    public void AddKillCount()
    {
        if (killCount < 10) 
        {
            killCount++;
            // killCountText.text = "Kill: " + killCount + "/10";

            // Kill indicator 이미지 변경
            if (killCount <= killIndicators.Count)
            {
                killIndicators[killCount - 1].texture = newTexture;
            }
        }

        if (killCount == 10)
        {
            Debug.Log("You Win");
            // winImage.gameObject.SetActive(true);
            // loseImage.gameObject.SetActive(false);
            // Win 씬으로 이동
            UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
        }
    }

    public void SubtractHpCount()
    {
        if (hpCount >= 0) 
        {
            hpCount--;
            // hpCountText.text = "HP: " + hpCount;
            UpdateHpSlider();
        }

        if (hpCount == 0) 
        {
            Debug.Log("Game Over");
            // loseImage.gameObject.SetActive(true);
            // winImage.gameObject.SetActive(false);
            // Lose 씬으로 이동
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
        }
    }

    public void PillarDestroyed()
    {
        if (pillarCount > 0) 
        {
            pillarCount--;
            // baseHpCountText.text = "Base (Pillar) HP: " + pillarCount + "/8";
        }

        if (pillarCount == 0) 
        {
            Debug.Log("Game Over");
            // loseImage.gameObject.SetActive(true);
            // winImage.gameObject.SetActive(false);
            // Lose 씬으로 이동
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
        }
    }

    private void UpdateHpSlider()
    {
        if (hpSlider != null)
        {
            hpSlider.value = hpCount; // 현재 HP 값을 Slider에 반영
        }
    }

    public void PillarHit()
    {
        baseTotalHp--;
        UpdateBaseHpSlider();
    }

    private void UpdateBaseHpSlider()
    {
        if (baseHpSlider != null)
        {
            baseHpSlider.value = baseTotalHp;
        }
    }
}
