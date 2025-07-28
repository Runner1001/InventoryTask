using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/TeleportRing")]
public class TeleportRing : Item, IAsyncItem
{
    //public override void Use(GameObject player)
    //{
    //    TeleportManager.Instance.Teleport(player, new Vector3(0, 1, 0));
    //}

    public IEnumerator UseAsync(GameObject player, Action onComplete)
    {
        if(TeleportManager.Instance.IsTeleporting)
            yield break;

        TeleportManager.Instance.Teleport(player, new Vector3(0, 1, 0));

        yield return new WaitUntil(() => !TeleportManager.Instance.IsTeleporting);

        onComplete.Invoke();
    }
}
