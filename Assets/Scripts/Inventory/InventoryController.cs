using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public InventorySo inventory; // Envanter verisi
    private int currentWeaponIndex = 0; // Ba�lang��ta g�sterilecek silah
    public GameObject[] weaponModels; // Silah modelleri (Prefab'lar)
    private GameObject currentWeaponModel;

    void Start()
    {
        // Ba�lang��ta ilk silah� g�ster
        SwitchWeapon(currentWeaponIndex);
    }

    void Update()
    {
        // Scroll yukar� (yeni silah se�)
        if (Input.mouseScrollDelta.y > 0)
        {
            currentWeaponIndex++;
            if (currentWeaponIndex >= inventory.inventorySlots.Count)
                currentWeaponIndex = 0; // Silahlar aras�nda d�ng�
            SwitchWeapon(currentWeaponIndex);
        }
        // Scroll a�a�� (�nceki silah se�)
        else if (Input.mouseScrollDelta.y < 0)
        {
            currentWeaponIndex--;
            if (currentWeaponIndex < 0)
                currentWeaponIndex = inventory.inventorySlots.Count - 1; // D�ng� ba��na d�n
            SwitchWeapon(currentWeaponIndex);
        }
    }

    void SwitchWeapon(int index)
    {
        // E�er aktif bir silah varsa devre d��� b�rak
        if (currentWeaponModel != null)
        {
            currentWeaponModel.SetActive(false);  // Eski silah� gizle
        }

        // Yeni silah� aktif et
        if (index < weaponModels.Length && weaponModels[index] != null)
        {
            currentWeaponModel = weaponModels[index];  // Yeni silah modelini se�
            currentWeaponModel.SetActive(true);         // Yeni silah� g�ster
        }

        // E�er silah modelinizin animasyonu varsa, buraya animasyon oynatmay� da ekleyebilirsiniz
        // �rnek: currentWeaponModel.GetComponent<Animator>().Play("DrawWeapon");
    }
}
