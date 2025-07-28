using UnityEngine;

[CreateAssetMenu(menuName = "Items/Apple")]
public class Apple : Item
{
    public int HealAmount;

    public override string GetDescription()
    {
        return $"{Description}: <color=green>{HealAmount}</color> HP ";
    }

    public override void Use(GameObject player)
    {
        if(player.TryGetComponent(out Health health))
        {
            health.SetHealth(HealAmount);
        }
    }
}
