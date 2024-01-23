using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Camera mainCamera;
    public Text priceText;
    public Text nameText;
    public Text hintText;

    private void Start()
    {
        SetEquippedWeapon(PlayerPrefs.GetInt("EquippedWeaponId"));
    }
    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            ShopItem shopItem = hitObject.GetComponent<ShopItem>();

            if (shopItem != null)
            {
                int cost = shopItem.GetCost();
                string itemName = shopItem.GetName();
                priceText.text = "Cost: " + cost.ToString()+"$";
                nameText.text = itemName;
                if (!shopItem.GetStatus())
                {
                    hintText.text = "Press (E) to buy";
                }
                else
                {
                    hintText.text = "Press (E) to equip";
                }



                if (Input.GetMouseButtonDown(0) && CanPurchase(cost))
                {
                    PurchaseObject(shopItem, cost);
                }
            }
            else
            {
                priceText.text = "";
                nameText.text = "";
                hintText.text = "";
            }
        }
        else
        {
            priceText.text = "";
            nameText.text = "";
            hintText.text = "";
        }
    }

    private bool CanPurchase(int cost)
    {
        // Логика проверки, может ли игрок себе позволить покупку
        return true;
    }

    private void PurchaseObject(ShopItem shopItem, int cost)
    {
        int id = shopItem.GetId();

        Debug.Log("Item purchased!");
        shopItem.Purchase();

        // Save purchased item state and ID in PlayerPrefs
        PlayerPrefs.SetInt("Item" + id + "Purchased", 1);
        PlayerPrefs.SetInt("EquippedWeaponId", id);

        SetEquippedWeapon(id);
    }

    private void SetEquippedWeapon(int id)
    {
        ArsenalManager arsenal = FindObjectOfType<ArsenalManager>();
        arsenal.pistol.SetActive(false);
        arsenal.shotgun.SetActive(false);
        arsenal.rifle.SetActive(false);
        arsenal.sniperrifle.SetActive(false);
        arsenal.Flamethrower.SetActive(false);

        switch (id)
        {
            case 0:
                arsenal.pistol.SetActive(true);
                break;
            case 1:
                arsenal.shotgun.SetActive(true);
                break;
            case 2:
                arsenal.rifle.SetActive(true);
                break;
            case 3:
                arsenal.sniperrifle.SetActive(true);
                break;
            case 4:
                arsenal.Flamethrower.SetActive(true);
                break;
        }
    }
}