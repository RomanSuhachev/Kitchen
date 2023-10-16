using TMPro;
using UnityEngine;
using Image = UnityEngine.UIElements.Image;

public class DeliveryManagerSingleUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI recipeNameText;
   [SerializeField] private Transform iconContainer;
   [SerializeField] private Transform iconTemplate;

   private void Awake()
   {
      iconTemplate.gameObject.SetActive(false);
   }

   // ReSharper disable Unity.PerformanceAnalysis
   public void SetRecipeSO(RecipeSO recipeSO)
   {
      recipeNameText.text = recipeSO.recipeName;

      foreach (Transform child in iconContainer)
      {
         if(child == iconTemplate) continue;
         Destroy(child.gameObject);
      }

      foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
      {
        Transform iconTransform =  Instantiate(iconTemplate, iconContainer);
        iconTransform.gameObject.SetActive(true);
        iconTransform.GetComponent<UnityEngine.UI.Image>().sprite = kitchenObjectSO.sprite;
      }
   }
}
