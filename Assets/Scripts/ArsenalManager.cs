using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArsenalManager : MonoBehaviour
{
    public GameObject pistol;
    public GameObject rifle;
    public GameObject sniperrifle;
    public GameObject shotgun;
    public GameObject Flamethrower;
    void Start()
    {
        SetEquippedWeapon(PlayerPrefs.GetInt("EquippedWeaponId"));
        Debug.Log("EquippedWeaponId");
    }
    private void SetEquippedWeapon(int id)
    {
        pistol.SetActive(false);
        shotgun.SetActive(false);
        rifle.SetActive(false);
        sniperrifle.SetActive(false);
        Flamethrower.SetActive(false);

        switch (id)
        {
            case 0:
                pistol.SetActive(true);
                break;
            case 1:
                shotgun.SetActive(true);
                break;
            case 2:
                rifle.SetActive(true);
                break;
            case 3:
                sniperrifle.SetActive(true);
                break;
            case 4:
                Flamethrower.SetActive(true);
                break;
        }
    }
}
