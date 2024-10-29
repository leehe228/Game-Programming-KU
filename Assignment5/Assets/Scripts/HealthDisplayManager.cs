using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplayManager : MonoBehaviour
{
    public static HealthDisplayManager Instance;

    [System.Serializable]
    public class PillarHealthUI
    {
        public Image circleImage;
        public TextMeshProUGUI hpText;
    }

    public PillarHealthUI[] pillarHealthUIs;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePillarHealth(int index, int currentHP)
    {
        if (index < 0 || index >= pillarHealthUIs.Length) return;

        PillarHealthUI pillarUI = pillarHealthUIs[index];
        pillarUI.hpText.text = currentHP.ToString();

        // Change color from #FF0000 to #000000 based on HP
        float healthRatio = (float)currentHP / 10f;
        pillarUI.circleImage.color = Color.Lerp(Color.white, new Color(1f, 0f, 0f), healthRatio);

        if (currentHP == 0)
        {
            pillarUI.hpText.text = "X";
            pillarUI.circleImage.color = Color.black;
        }
    }
}