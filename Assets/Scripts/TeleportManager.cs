using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : MonoBehaviour
{
    public static string lastTeleportName;

    public static void Teleport(string teleportName, bool isShop)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(!isShop)
        {
            lastTeleportName = teleportName;
            SceneManager.LoadScene("Shop");
        }
        else
        {
            SceneManager.LoadScene("LevelDesign");
        }

    }
}
