using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManagerEvent : MonoBehaviour
{
    //New variable to store the instance of the UIManager
    public static UIManagerEvent Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI ammoText;

    //Does the same thing
    //Unity event
    public UnityEvent<int, int> OnAmmoChangeUnityEvent = new UnityEvent<int, int>();
    //C# event
    public Action<int, int> OnAmmoChangeAction;

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
    private void OnEnable()
    {
        OnAmmoChangeAction += UpdateAmmoUI;
        OnAmmoChangeUnityEvent.AddListener(UpdateAmmoUI);
    }
    private void OnDisable()
    {
        OnAmmoChangeAction -= UpdateAmmoUI;
        OnAmmoChangeUnityEvent.RemoveListener(UpdateAmmoUI);
    }

    public void UpdateAmmoUI(int currentAmmo, int maxAmmo)
    {
        // The {} is a string interpolation, it allows you to insert variables into a string
        // Like enum, string ,int, etc
        ammoText.text = $"{currentAmmo}/{maxAmmo}";
    }
}
