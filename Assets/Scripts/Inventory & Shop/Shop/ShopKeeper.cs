using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopKeeper : MonoBehaviour
{
    public static ShopKeeper currentShopKeeper;
    public Animator anim;
    public CanvasGroup shopCanvasGroup;
    public ShopManager shopManager;

    private bool playerInRange;
    private bool isShopOpen;

    [SerializeField] private List<ShopItems> shopItems;
    [SerializeField] private List<ShopItems> shopWeapons;
    [SerializeField] private List<ShopItems> shopArmour;
    public static event Action<ShopManager, bool> onShopStateChanged;

    [SerializeField] private Camera shopKeeperCam;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0,0,-1);


    private void Awake()
    {
        shopCanvasGroup = GameObject.Find("ShopCanvas").GetComponent<CanvasGroup>();
        shopManager = GameObject.Find("ShopCanvas").GetComponent<ShopManager>();
        shopKeeperCam = GameObject.FindGameObjectWithTag("ShopKeeperCam").GetComponent<Camera>();
    }

    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (!isShopOpen)
                {
                    Time.timeScale = 0;
                    currentShopKeeper = this;
                    isShopOpen = true;
                    shopCanvasGroup.alpha = 1;
                    shopCanvasGroup.blocksRaycasts = true;
                    shopCanvasGroup.interactable = true;
                    onShopStateChanged?.Invoke(shopManager, true);
                    shopKeeperCam.transform.position = transform.position + cameraOffset;
                    shopKeeperCam.gameObject.SetActive(true);
                    OpenItemShop();
                }
            }
            else if (Input.GetButtonDown("Cancel"))
            {
                if (isShopOpen)
                {
                    Time.timeScale = 1;
                    isShopOpen = false;
                    currentShopKeeper = null;
                    shopCanvasGroup.alpha = 0;
                    shopCanvasGroup.blocksRaycasts = false;
                    shopCanvasGroup.interactable = false;
                    onShopStateChanged?.Invoke(shopManager, false);
                    shopKeeperCam.gameObject.SetActive(false);

                }
            }
        }
    }

    public void OpenItemShop()
    {
        shopManager.PopulateShopItems(shopItems);
    }

    public void OpenWeaponShop()
    {
        shopManager.PopulateShopItems(shopWeapons);
    }

    public void OpenArmourShop()
    {
        shopManager.PopulateShopItems(shopArmour);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("playerInRange", true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("playerInRange", false);
            playerInRange = false;
        }
    }
}
