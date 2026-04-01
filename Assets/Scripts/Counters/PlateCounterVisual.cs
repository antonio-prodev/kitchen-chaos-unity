using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform plateVisual;
    [SerializeField] private Transform counterTopPoint;

    private List<Transform> spawnedPlatesList;

    private void Awake()
    {
        spawnedPlatesList = new List<Transform>();
    }

    private void Start()
    {
        plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        Transform plate = spawnedPlatesList[spawnedPlatesList.Count - 1];
        spawnedPlatesList.Remove(plate);
        Destroy(plate.gameObject);
    }

    private void PlateCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        Transform plate = Instantiate(plateVisual, counterTopPoint);
        float yOffset = .1f;
        plate.transform.localPosition = new Vector3(0,spawnedPlatesList.Count * yOffset,0);
        spawnedPlatesList.Add(plate);
    }
}
