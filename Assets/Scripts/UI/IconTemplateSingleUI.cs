using UnityEngine;
using UnityEngine.UI;

public class IconTemplateSingleUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        iconImage.sprite = kitchenObjectSO.sprite;
    }
}
