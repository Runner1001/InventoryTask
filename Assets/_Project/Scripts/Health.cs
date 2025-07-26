using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] UnityEvent<float, float> onHealthChange;

    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth * 0.5f; //Setting half life just for the demo

        onHealthChange.Invoke(currentHealth, maxHealth);
    }

    public void SetHealth(float amount)
    {
        currentHealth += amount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        onHealthChange.Invoke(currentHealth, maxHealth);
    }
}
