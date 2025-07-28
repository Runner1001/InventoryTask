using UnityEngine;

public class GameSingleton<T> : MonoBehaviour where T : GameSingleton<T>
{
    public static T Instance {  get; private set; }

    public virtual void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);

        Instance = (T)this;
    }
}
