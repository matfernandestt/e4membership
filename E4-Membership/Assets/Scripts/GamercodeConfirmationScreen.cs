using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamercodeConfirmationScreen : MonoBehaviour
{
    [SerializeField] private UserRegistration registration;

    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI gamercodeField;
    [SerializeField] private Button confirmationButton;

    private void Awake()
    {
        confirmationButton.onClick.AddListener(registration.CompleteRegistration);
        DisabledState();
    }

    public void SetGamercode(string code)
    {
        panel.SetActive(true);
        confirmationButton.gameObject.SetActive(false);
        confirmationButton.interactable = true;
        gamercodeField.text = code;

        IEnumerator EnableConfirmation()
        {
            yield return new WaitForSeconds(2f);
            confirmationButton.gameObject.SetActive(true);
        }
        StartCoroutine(EnableConfirmation());
    }

    public void DisabledState()
    {
        panel.SetActive(false);
        confirmationButton.interactable = true;
        confirmationButton.gameObject.SetActive(false);
    }
}
