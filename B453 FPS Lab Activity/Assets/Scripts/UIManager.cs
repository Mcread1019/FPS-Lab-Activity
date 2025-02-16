//Imported TMPro
using TMPro;
//----------------------------------------------
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //New variable to store the instance of the UIManager
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI ammoText;

    //Added awake method to set the instance
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ammoText = GameObject.Find("Ammo Count Text").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateAmmoUI(int currentAmmo, int maxAmmo)
    {
        // The {} is a string interpolation, it allows you to insert variables into a string
        // Like enum, string ,int, etc
        ammoText.text = $"{currentAmmo}/{maxAmmo}";
    }
}
