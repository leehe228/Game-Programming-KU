using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSIndicator : MonoBehaviour
{
    public TextMeshProUGUI fpsText; // TextMeshPro UI 텍스트를 연결
    private float deltaTime;

    void Update()
    {
        // 델타 타임 누적
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // FPS 계산
        float fps = 1.0f / deltaTime;

        // 텍스트 업데이트
        fpsText.text = string.Format("FPS: {0:0.00}", fps);
    }
}
