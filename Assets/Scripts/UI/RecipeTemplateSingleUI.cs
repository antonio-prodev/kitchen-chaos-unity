using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeTemplateSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach(Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform template = Instantiate(iconTemplate, iconContainer);
            template.gameObject.SetActive(true);
            template.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
