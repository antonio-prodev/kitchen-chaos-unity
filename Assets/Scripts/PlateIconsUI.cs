using System;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform imageTemplateTransform;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        imageTemplateTransform.gameObject.SetActive(false);
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdatePlateIcons();
    }

    private void UpdatePlateIcons()
    {
        foreach (Transform child in transform)
        {
            if (child == imageTemplateTransform) continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform imageTemplate = Instantiate(imageTemplateTransform, transform);
            imageTemplate.gameObject.SetActive(true);
            imageTemplate.GetComponent<ImageTemplateSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
