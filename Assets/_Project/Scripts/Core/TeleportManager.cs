using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeleportManager : GameSingleton<TeleportManager>
{
    [SerializeField] Slider teleportBar;
    [SerializeField] TMP_Text teleportText;
    [SerializeField] float duration = 2f;

    Tween teleportTween;
    Tween counterTween;

    public bool IsTeleporting { get; private set; }

    public void Teleport(GameObject objectToTeleport, Vector3 destination)
    {
        IsTeleporting = true;
        teleportBar.value = 0f;
        teleportBar.gameObject.SetActive(true);

        counterTween = DOVirtual.Float(duration, 0, duration, (value) =>
        {
            teleportText.SetText($"Teleporting in {value:F1}");
        });

        teleportTween = teleportBar.DOValue(1f, duration).OnComplete(() =>
        {
            IsTeleporting = false;
            teleportBar.gameObject.SetActive(false);
            objectToTeleport.transform.position = destination;
        });
    }

    public void StopTeleport()
    {
        IsTeleporting = false;
        teleportTween?.Kill();
        counterTween?.Kill();
    }
}
