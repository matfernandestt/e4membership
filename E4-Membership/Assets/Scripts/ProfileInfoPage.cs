using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileInfoPage : MonoBehaviour
{
    [SerializeField] private GameObject windowEnabler;
    
    [SerializeField] private RawImage profileImage;
    [SerializeField] private TextMeshProUGUI profileUsername;
    [SerializeField] private TextMeshProUGUI profileGamercode;
    [SerializeField] private Button screenLockerButton;

    private void Awake()
    {
        CloseProfilePage();
        
        screenLockerButton.onClick.AddListener(CloseProfilePage);
    }

    public void SetupProfile(Texture2D photo, string username, string gamercode)
    {
        profileImage.texture = photo;
        profileUsername.text = username;
        profileGamercode.text = gamercode;
        
        windowEnabler.SetActive(true);
    }

    private void CloseProfilePage()
    {
        windowEnabler.SetActive(false);
    }
}
