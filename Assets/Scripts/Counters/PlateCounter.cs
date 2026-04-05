using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    [SerializeField] private float spawnPlateInterval = 4.0f;
    [SerializeField] private int plateMaxAmount = 4;


    private float plateSpawnTimer;
    private int plateSpawnAmount;
    
    

    private void Update()
    {
        plateSpawnTimer += Time.deltaTime;
        if (plateSpawnTimer > spawnPlateInterval)
        {
            plateSpawnTimer = 0f;
            
            if(GameManager.Instance.IsGamePlaying() && plateSpawnAmount < plateMaxAmount)
            {
                plateSpawnAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            // Player is free handed
            if (plateSpawnAmount > 0)
            {
                // There is atleast one plate on the counter
                plateSpawnAmount --;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
