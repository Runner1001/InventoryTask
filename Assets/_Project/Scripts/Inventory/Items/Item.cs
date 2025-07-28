using UnityEngine;

public abstract class Item : ScriptableObject
{
    public int Id;
    public Sprite Icon;
    public string Name;
    public string Description;
    public string UseInfo;
    public int MaxStackSize;

    public virtual string GetDescription() => Description;

    public virtual void Use(GameObject player)
    {

    }
}
