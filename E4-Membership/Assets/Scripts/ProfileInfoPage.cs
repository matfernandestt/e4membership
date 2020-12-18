using System;
using System.Collections;
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
    [SerializeField] private GameObject scrollViewLock;

    [SerializeField] private Transform scrollContents;
    [SerializeField] private Button addNewLikeButton;
    [SerializeField] private AddNewGameScreen addNewGameScreen;

    [SerializeField] private GameSlot gameSlot;

    private void Awake()
    {
        CloseProfilePage();
        
        screenLockerButton.onClick.AddListener(CloseProfilePage);
        addNewLikeButton.onClick.AddListener(OpenAddNewGameScreen);
    }

    public void OpenProfilePage(string photoUrl, Texture2D photo, string username, string gamercode)
    {
        addNewLikeButton.gameObject.SetActive(gamercode == UserData.gamercode);

        StartCoroutine(ParsePhoto(photoUrl));
        profileImage.texture = photo;
        profileUsername.text = username;
        profileGamercode.text = gamercode;
        
        windowEnabler.SetActive(true);
        
        RefreshUserLikes();
    }
    
    private IEnumerator ParsePhoto(string url)
    {
        var wwwPhoto = new WWW(url);
        yield return wwwPhoto;
        profileImage.texture = wwwPhoto.texture;
        wwwPhoto.Dispose();
    }

    private void CloseProfilePage()
    {
        windowEnabler.SetActive(false);
        scrollViewLock.SetActive(false);
    }

    private void OpenAddNewGameScreen()
    {
        addNewGameScreen.OpenSreen();
    }

    public void AddLike(string gameName)
    {
        StartCoroutine(AddingLike(gameName));
    }

    private IEnumerator AddingLike(string gameName)
    {
        var gamerCode = profileGamercode.text;
        var form = new WWWForm();
        form.AddField("gamercode", gamerCode);
        form.AddField("gamename", gameName);
        
        form.headers["Access-Control-Allow-Credentials"] = "true";
        form.headers["Access-Control-Allow-Headers"] = "Accept, Content-Type, X-Access-Token, X-Application-Name, X-Request-Sent-Time";
        form.headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, OPTIONS";
        form.headers["Access-Control-Allow-Origin"] = "*";
        
        var request = new WWW(ServerAddresses.AddUserLikesAddress, form);
        yield return request;
        if (request.text == "0")
        {
            RefreshUserLikes();
        }
    }

    private void RefreshUserLikes()
    {
        scrollViewLock.SetActive(true);
        EraseContents();
        StartCoroutine(RefreshingUserLikes());
    }

    private IEnumerator RefreshingUserLikes()
    {
        var gamerCode = profileGamercode.text;
        var form = new WWWForm();
        form.AddField("gamercode", gamerCode);
        
        form.headers["Access-Control-Allow-Credentials"] = "true";
        form.headers["Access-Control-Allow-Headers"] = "Accept, Content-Type, X-Access-Token, X-Application-Name, X-Request-Sent-Time";
        form.headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, OPTIONS";
        form.headers["Access-Control-Allow-Origin"] = "*";
        
        var request = new WWW(ServerAddresses.MemberProfileAddress, form);
        yield return request;
        if (request.text == "1")
        {
        }
        else
        {
            var result = request.text.Split('@');
            for (var i = 0; i < result.Length - 1; i++)
            {
                CreateEmptyButton().SetGame(result[i]);
            }
        }
        scrollViewLock.SetActive(false);
    }
    
    private GameSlot CreateEmptyButton()
    {
        return Instantiate(gameSlot, scrollContents);
    }
    
    private void EraseContents()
    {
        foreach (Transform child in scrollContents)
        {
            Destroy(child.gameObject);
        }
    }
}
