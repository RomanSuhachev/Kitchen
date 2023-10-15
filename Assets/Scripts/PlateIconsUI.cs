using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
  [SerializeField] private PlateKitchenObject plateKitchenObject;
  [SerializeField] private Transform iconTemplate;

  private void Awake()
  {
    iconTemplate.gameObject.SetActive(false);
  }

  private void Start()
  {
    plateKitchenObject.OnIngridientAdded += PlateKitchenObject_OnIngridientAdded;
  }

  private void PlateKitchenObject_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
  {
    UpdateVisual();
  }

  private void UpdateVisual()
  {
    foreach (Transform child in transform)
    {
      if (child == iconTemplate) continue;
      Destroy(child.gameObject);
    }
    foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
    {
      Transform iconTransform = Instantiate(iconTemplate, transform);
      iconTemplate.gameObject.SetActive(true);
      iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
    }
  }
}
