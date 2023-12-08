using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;
    private IShopCustomer shopCustomer;

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {   
        CreateItemButton(Item.ItemType.Limbs_1, Item.GetSprite(Item.ItemType.Limbs_1), "Limbs 1", Item.GetCost(Item.ItemType.Limbs_1), 0);
        CreateItemButton(Item.ItemType.Limbs_2, Item.GetSprite(Item.ItemType.Limbs_2), "Limbs 2", Item.GetCost(Item.ItemType.Limbs_2), 1);
    }
    private void CreateItemButton(Item.ItemType itemType, Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 30f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

        shopItemTransform.GetComponent<UI_Button>().ClickFunc = () =>
        {
            //TryBuyItem(itemType);
        };

    }

    /*private void TryBuyItem(Item.ItemType itemType)
    {
        if (shopCustomer.TrySpendGoldAmount(Item.GetCost(itemType)))
        {
            // Can afford cost
            shopCustomer.BoughtItem(itemType);
        }
    }*/

    public void Show(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true); 
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
