using UnityEngine;

public class Item : MonoBehaviour
{
    public WeaponSO weapon;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponentInChildren<Inventory>();
            if (inventory != null)
            {
                inventory.PlayerInventory.AddItem(weapon);
                Destroy(gameObject);
            }
        }
    }
}
