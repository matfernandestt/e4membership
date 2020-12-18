﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UserRegistration : MonoBehaviour
{
    public Action OnCompleteRegistration;
    public Action OnClickGoBack;
    
    [SerializeField] private TMP_InputField usernameTextField;
    [SerializeField] private TMP_InputField imageUrlField;
    [SerializeField] private Button eraseUrlButton;
    [SerializeField] private Button pasteClipboardButton;

    [SerializeField] private Button registerButton;
    [SerializeField] private GamercodeConfirmationScreen confirmationScreen;

    [SerializeField] private GameObject loadingLockScreen;
    
    [SerializeField] private Button backButton;

    private void Awake()
    {
        registerButton.interactable = false;
        registerButton.onClick.AddListener(Register);
        
        eraseUrlButton.onClick.AddListener(ErasePhotoUrl);
        eraseUrlButton.gameObject.SetActive(false);
        
        pasteClipboardButton.onClick.AddListener(() => imageUrlField.text = GUIUtility.systemCopyBuffer);
        
        backButton.onClick.AddListener(GoBack);
        backButton.gameObject.SetActive(true);
        
        loadingLockScreen.SetActive(false);
    }

    private void GoBack()
    {
        OnClickGoBack?.Invoke();
    }

    private void Register()
    {
        backButton.gameObject.SetActive(false);
        StartCoroutine(ConnectionCoroutine());
    }

    private IEnumerator ConnectionCoroutine()
    {
        loadingLockScreen.SetActive(true);
        registerButton.interactable = false;
        var form = new WWWForm();
        var randomValue = Random.Range(0, 999999).ToString("000 000");
        form.AddField("gamercode", randomValue);
        form.AddField("username", usernameTextField.text);
        form.AddField("photo", imageUrlField.text);
        
        form.headers["Access-Control-Allow-Credentials"] = "true";
        form.headers["Access-Control-Allow-Headers"] = "Accept, Content-Type, X-Access-Token, X-Application-Name, X-Request-Sent-Time";
        form.headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, OPTIONS";
        form.headers["Access-Control-Allow-Origin"] = "*";
        
        var www = new WWW(ServerAddresses.RegisterPageAddress, form);
        yield return www;
        if (www.text == "0")
        {
            var wwwPhoto = new WWW(imageUrlField.text);
            yield return wwwPhoto;
            UserData.gamercode = randomValue;
            UserData.username = usernameTextField.text;
            UserData.photo = imageUrlField.text;
            UserData.photoTexture = wwwPhoto.texture;
            wwwPhoto.Dispose();
            
            confirmationScreen.SetGamercode(UserData.gamercode);
        }
        else
        {
            Debug.Log("ERROR: " + www.text);
        }

        usernameTextField.text = "";
        imageUrlField.text = "";
        loadingLockScreen.SetActive(false);
    }

    public void CompleteRegistration()
    {
        backButton.gameObject.SetActive(true);
        confirmationScreen.DisabledState();
        OnCompleteRegistration?.Invoke();
    }

    private void ErasePhotoUrl()
    {
        imageUrlField.text = "";
    }

    public void ValidateFields()
    {
        registerButton.interactable = (usernameTextField.text.Length > 0 && imageUrlField.text.Length > 0);
    }
    
    public void ValidatePhotoUrl()
    {
        eraseUrlButton.gameObject.SetActive(imageUrlField.text.Length > 0);
    }
}
