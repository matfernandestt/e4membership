using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MemberViewPage : MonoBehaviour
{
    public Action OnClickRegisterButton;
    public Action OnClickLoginButton;
    
    public ProfileInfoPage profileInfoPage;
    
    [SerializeField] private Button refreshButton;
    [SerializeField] private Transform scrollContents;
    [SerializeField] private GameObject scrollbarLock;

    [SerializeField] private MemberInList memberButton;

    [SerializeField] private Button myProfile;
    [SerializeField] private RawImage myProfilePicture;
    [SerializeField] private TextMeshProUGUI myName;
    [SerializeField] private Button logoffButton;

    [SerializeField] private GameObject guestTab;
    [SerializeField] private Button registerNowButton;
    [SerializeField] private Button loginButton;
    
    private void Awake()
    {
        refreshButton.onClick.AddListener(RefreshList);
        scrollbarLock.SetActive(false);
        
        myProfile.onClick.AddListener(() => profileInfoPage.OpenProfilePage(UserData.photoTexture, UserData.username, UserData.gamercode));
        logoffButton.onClick.AddListener(LoginEvent);
        
        registerNowButton.onClick.AddListener(RegisterEvent);
        loginButton.onClick.AddListener(LoginEvent);
    }

    private void RegisterEvent()
    {
        OnClickRegisterButton?.Invoke();
    }

    private void LoginEvent()
    {
        OnClickLoginButton?.Invoke();
        UserData.LogOff();
    }

    public void RefreshList()
    {
        scrollbarLock.SetActive(true);
        refreshButton.interactable = false;
        StartCoroutine(RefreshingList());

        if (UserData.IsLoggedIn)
        {
            myProfilePicture.texture = UserData.photoTexture;
            myName.text = UserData.username;
        }
        myProfile.gameObject.SetActive(UserData.IsLoggedIn);
        logoffButton.gameObject.SetActive(UserData.IsLoggedIn);
        guestTab.SetActive(!UserData.IsLoggedIn);
    }

    private IEnumerator RefreshingList()
    {
        var request = new WWW(ServerAddresses.MembersPageAddress);
        yield return request;
        EraseContents();
        string[] requestReturn = request.text.Split('*');
        for (var i = 0; i < requestReturn.Length - 1; i++)
        {
            var row = requestReturn[i];
            var newMember = CreateEmptyButton();
            string[] info = row.Split('@');
            newMember.SetupPlayer(info[0], info[1], info[2], info[3], this);
        }

        refreshButton.interactable = true;
        scrollbarLock.SetActive(false);
    }

    private MemberInList CreateEmptyButton()
    {
        return Instantiate(memberButton, scrollContents);
    }

    private void EraseContents()
    {
        foreach (Transform child in scrollContents)
        {
            Destroy(child.gameObject);
        }
    }
}
