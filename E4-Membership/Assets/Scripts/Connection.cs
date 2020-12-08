using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Connection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI connectionText;
    [SerializeField] private TMP_InputField usernameTextField;
    [SerializeField] private TMP_InputField imageUrlField;
    
    [SerializeField] private Button registerButton;

    [SerializeField] private RawImage profileImage;

    private void Awake()
    {
        registerButton.interactable = false;
        registerButton.onClick.AddListener(Connect);
    }

    public void Connect()
    {
        StartCoroutine(ConnectionCoroutine());
    }

    private IEnumerator ConnectionCoroutine()
    {
        registerButton.interactable = false;
        WWWForm form = new WWWForm();
        var randomValue = Random.Range(0, 999999).ToString("000 000");
        form.AddField("gamercode", randomValue);
        form.AddField("username", usernameTextField.text);
        form.AddField("photo", imageUrlField.text);
        
        var www = new WWW("http://localhost/sqlconnect/register.php", form);
        yield return www;
        if (www.text == "0")
        {
            connectionText.text = "SUCCESS";
        }
        else
        {
            connectionText.text = "ERROR: " + www.text;
        }
        SetImage(imageUrlField.text);
    }

    public void SetImage(string url)
    {
        IEnumerator SetImageCoroutine()
        {
            WWW www = new WWW(url);
            yield return www;
            profileImage.texture = www.texture;
            www.Dispose();
        }
        
        StartCoroutine(SetImageCoroutine());
    }

    public void ValidateFields()
    {
        registerButton.interactable = (usernameTextField.text.Length > 0 && imageUrlField.text.Length > 0);
    }
}
