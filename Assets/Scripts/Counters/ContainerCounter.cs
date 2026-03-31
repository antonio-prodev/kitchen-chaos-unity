using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnContainerCounterInteract;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            OnContainerCounterInteract?.Invoke(this, EventArgs.Empty);
        }
    }
}
