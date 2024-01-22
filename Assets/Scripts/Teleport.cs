using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string teleportName;
    public bool isInShop;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportManager.Teleport(teleportName, isInShop);

        }
    }
}