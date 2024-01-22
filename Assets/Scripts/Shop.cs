using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Camera mainCamera;
    public Text priceText;
    public Text nameText;
    public Text hintText;


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
                priceText.text = ""; // Скрываем текст, если не наведено на товар
                nameText.text = "";
                hintText.text = "";
            }
        }
        else
        {
            priceText.text = ""; // Скрываем текст, если не наведено на объект
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
        ArsenalManager arsenal = FindObjectOfType<ArsenalManager>();
        int id = shopItem.GetId();
        // Логика покупки товара
        Debug.Log("Item purchased!");
        shopItem.Purchase();
        if (id==0)
        {
            arsenal.pistol.SetActive(true);
            arsenal.shotgun.SetActive(false);
            arsenal.rifle.SetActive(false);
            arsenal.sniperrifle.SetActive(false);
            arsenal.Flamethrower.SetActive(false);
        }
        else if (id==1)
        {
            arsenal.pistol.SetActive(false);
            arsenal.shotgun.SetActive(true);
            arsenal.rifle.SetActive(false);
            arsenal.sniperrifle.SetActive(false);
            arsenal.Flamethrower.SetActive(false);
        }
        else if (id == 2)
        {
            arsenal.pistol.SetActive(false);
            arsenal.shotgun.SetActive(false);
            arsenal.rifle.SetActive(true);
            arsenal.sniperrifle.SetActive(false);
            arsenal.Flamethrower.SetActive(false);
        }
        else if (id == 3)
        {
            arsenal.pistol.SetActive(false);
            arsenal.shotgun.SetActive(false);
            arsenal.rifle.SetActive(false);
            arsenal.sniperrifle.SetActive(true);
            arsenal.Flamethrower.SetActive(false);
        }
        else if (id == 4)
        {
            arsenal.pistol.SetActive(false);
            arsenal.shotgun.SetActive(false);
            arsenal.rifle.SetActive(false);
            arsenal.sniperrifle.SetActive(false);
            arsenal.Flamethrower.SetActive(true);
        }
    }
}