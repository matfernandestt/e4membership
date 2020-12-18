using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddNewGameScreen : MonoBehaviour
{
    [SerializeField] private ProfileInfoPage profileInfoPage;
    [SerializeField] private GameObject screenPage;

    [SerializeField] private TMP_InputField newGameInputField;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        CloseScreen();
        
        confirmButton.onClick.AddListener(AddGame);
        closeButton.onClick.AddListener(CloseScreen);
    }

    public void OpenSreen()
    {
        confirmButton.interactable = false;
        newGameInputField.text = "";
        screenPage.SetActive(true);
    }

    public void CloseScreen()
    {
        screenPage.SetActive(false);
    }

    private void AddGame()
    {
        confirmButton.interactable = false;
        profileInfoPage.AddLike(newGameInputField.text);
        CloseScreen();
    }

    public void ValidateInput()
    {
        confirmButton.interactable = newGameInputField.text.Length > 0;
    }
}
