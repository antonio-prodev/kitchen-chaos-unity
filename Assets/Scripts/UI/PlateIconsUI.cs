using System;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplateTransform;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        iconTemplateTransform.gameObject.SetActive(false);
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdatePlateIcons();
    }

    private void UpdatePlateIcons()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplateTransform) continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTemplate = Instantiate(iconTemplateTransform, transform);
            iconTemplate.gameObject.SetActive(true);
            iconTemplate.GetComponent<IconTemplateSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
