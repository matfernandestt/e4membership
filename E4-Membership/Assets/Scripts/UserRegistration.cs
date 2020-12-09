using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UserRegistration : MonoBehaviour
{
    public Action OnCompleteRegistration;
    
    [SerializeField] private TMP_InputField usernameTextField;
    [SerializeField] private TMP_InputField imageUrlField;
    [SerializeField] private Button eraseUrlButton;

    [SerializeField] private Button registerButton;
    [SerializeField] private GamercodeConfirmationScreen confirmationScreen;

    private void Awake()
    {
        registerButton.interactable = false;
        registerButton.onClick.AddListener(Connect);
        
        eraseUrlButton.onClick.AddListener(ErasePhotoUrl);
        eraseUrlButton.gameObject.SetActive(false);
    }

    private void Connect()
    {
        StartCoroutine(ConnectionCoroutine());
    }

    private IEnumerator ConnectionCoroutine()
    {
        registerButton.interactable = false;
        var form = new WWWForm();
        var randomValue = Random.Range(0, 999999).ToString("000 000");
        form.AddField("gamercode", randomValue);
        form.AddField("username", usernameTextField.text);
        form.AddField("photo", imageUrlField.text);
        
        var www = new WWW("http://localhost/sqlconnect/register.php", form);
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
    }

    public void CompleteRegistration()
    {
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
