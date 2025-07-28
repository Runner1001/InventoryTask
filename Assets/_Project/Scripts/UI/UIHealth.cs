using TMPro;
using UnityEngine;

public class UIHealth : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;

    //UnityEvent
    public void UpdateHealth(float currenHealth, float maxHealth)
    {
        healthText.SetText($"HP: {currenHealth}/{maxHealth}");
    }
}
