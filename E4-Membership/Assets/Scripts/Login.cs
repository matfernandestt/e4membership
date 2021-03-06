﻿using System;
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public Action OnLoginSuccessfull;
    public Action OnClickEnterAsGuest;
    public Action OnClickRegister;

    [SerializeField] private TMP_InputField gamerCodeInputField;

    [SerializeField] private TextMeshProUGUI invalidGamerCodeLabel;
    
    [SerializeField] private Button loginButton;
    [SerializeField] private Button enterAsGuestButton;
    [SerializeField] private Button registerButton;

    private void Awake()
    {
        loginButton.interactable = false;
        loginButton.onClick.AddListener(ClickLogin);

        enterAsGuestButton.interactable = true;
        enterAsGuestButton.onClick.AddListener(EnterAsGuest);

        registerButton.interactable = true;
        registerButton.onClick.AddListener(EnterRegisterPage);
        
        invalidGamerCodeLabel.gameObject.SetActive(false);
    }

    public void EnableLogin()
    {
        gamerCodeInputField.text = "";
        gamerCodeInputField.interactable = true;
        loginButton.interactable = false;
        enterAsGuestButton.interactable = true;
        registerButton.interactable = true;
    }

    private void ClickLogin()
    {
        gamerCodeInputField.interactable = false;
        loginButton.interactable = false;
        
        enterAsGuestButton.interactable = false;
        registerButton.interactable = false;
        
        StartCoroutine(LoggingIn());
    }

    private IEnumerator LoggingIn()
    {
        var regexGamerCode = gamerCodeInputField.text;
        regexGamerCode = Regex.Replace(regexGamerCode, " ", "");
        var usableGamerCode = "";
        for (var i = 0; i < regexGamerCode.ToCharArray().Length; i++)
        {
            var c = regexGamerCode.ToCharArray()[i];
            if (i == 3)
                usableGamerCode += " ";
            usableGamerCode += c;
        }
        var form = new WWWForm();
        form.AddField("gamercode", usableGamerCode);
        
        form.headers["Access-Control-Allow-Credentials"] = "true";
        form.headers["Access-Control-Allow-Headers"] = "Accept, Content-Type, X-Access-Token, X-Application-Name, X-Request-Sent-Time";
        form.headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, OPTIONS";
        form.headers["Access-Control-Allow-Origin"] = "*";
        
        var request = new WWW(ServerAddresses.LoginAddress, form);

        yield return request;

        if (request.text == "1")
        {
            gamerCodeInputField.text = "";
            invalidGamerCodeLabel.gameObject.SetActive(true);
            gamerCodeInputField.interactable = true;

            enterAsGuestButton.interactable = true;
            registerButton.interactable = true;
        }
        else
        {
            string[] requestReturn = request.text.Split('@');
            UserData.id = requestReturn[0];
            UserData.gamercode = requestReturn[1];
            UserData.username = requestReturn[2];
            UserData.photo = requestReturn[3];
            
            var wwwPhoto = new WWW(requestReturn[3]);
            yield return wwwPhoto;
            UserData.photoTexture = wwwPhoto.texture;
            wwwPhoto.Dispose();
            
            Debug.Log("LOGGED IN!");
            OnLoginSuccessfull?.Invoke();
        }
    }

    private void EnterAsGuest()
    {
        enterAsGuestButton.interactable = false;
        OnClickEnterAsGuest?.Invoke();
    }
    
    private void EnterRegisterPage()
    {
        registerButton.interactable = false;
        OnClickRegister?.Invoke();
    }

    public void ValidateGamerCode()
    {
        loginButton.interactable = gamerCodeInputField.text.Length >= 6;
        
        invalidGamerCodeLabel.gameObject.SetActive(false);
    }
}
