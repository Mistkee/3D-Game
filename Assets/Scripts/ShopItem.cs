using Unity.VisualScripting;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public int cost = 10; // Установите стоимость товара в инспекторе
    public string itemName;
    bool purchased;
    public int id;
    public int GetCost()
    {
        return cost;
    }
    public string GetName()
    {
        return itemName;
    }
    public bool GetStatus()
    {
        return purchased;
    }
    public int GetId()
    {
        return id;
    }
    public void Purchase()
    {
        // Логика покупки товара
        Debug.Log("Item purchased!");
        purchased = true;
    }
}
