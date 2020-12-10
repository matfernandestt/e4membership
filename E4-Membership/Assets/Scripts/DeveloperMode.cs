using UnityEngine;

public class DeveloperMode : MonoBehaviour
{
    public static bool DevModeEnabled;

    [SerializeField] private GameObject devModeScreen;

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
