using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonToggles : MonoBehaviour
{
    public void OpenItemShop()
    {
        Debug.Log("µã»÷1");

        if (ShopKeeper.currentShopKeeper != null)
        {
            Debug.Log("µã»÷2");
            ShopKeeper.currentShopKeeper.OpenItemShop();
        }
    }

    public void OpenWeaponsShop()
    {
        if (ShopKeeper.currentShopKeeper != null)
        {
            ShopKeeper.currentShopKeeper.OpenItemShop();
        }
    }

    public void OpenArmourShop()
    {
        if (ShopKeeper.currentShopKeeper != null)
        {
            ShopKeeper.currentShopKeeper.OpenArmourShop();
        }
    }
}
