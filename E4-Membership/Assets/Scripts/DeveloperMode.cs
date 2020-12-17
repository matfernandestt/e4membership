using System;
using TMPro;
using UnityEngine;

public class DeveloperMode : MonoBehaviour
{
    public static bool DevModeEnabled;

    [SerializeField] private GameObject devModeScreen;
    [SerializeField] private TextMeshProUGUI versionText;

    private void Awake()
    {
        versionText.text = "Version: " + Application.version;
        devModeScreen.SetActive(DevModeEnabled);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.L))
        {
            TurnDevMode();
        }
    }

    private void TurnDevMode()
    {
        DevModeEnabled = !DevModeEnabled;
        
        devModeScreen.SetActive(DevModeEnabled);
    }
}
