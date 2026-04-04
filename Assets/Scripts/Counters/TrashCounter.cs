using System;
using Unity.VisualScripting;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;

    new public static void ResectStaticData()
    {
        OnAnyObjectTrashed = null;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
