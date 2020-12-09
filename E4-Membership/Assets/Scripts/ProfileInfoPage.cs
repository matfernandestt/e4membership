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

    public void OpenProfilePage(Texture2D photo, string username, string gamercode)
    {
        addNewLikeButton.gameObject.SetActive(gamercode == UserData.gamercode);
        
        profileImage.texture = photo;
        profileUsername.text = username;
        profileGamercode.text = gamercode;
        
        windowEnabler.SetActive(true);
        
        RefreshUserLikes();
    }

    private void CloseProfilePage()
    {
        windowEnabler.SetActive(false);
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
        var request = new WWW("http://localhost/sqlconnect/addnewuserlike.php", form);
        yield return request;
        if (request.text == "0")
        {
            RefreshUserLikes();
        }
    }

    private void RefreshUserLikes()
    {
        EraseContents();
        StartCoroutine(RefreshingUserLikes());
    }

    private IEnumerator RefreshingUserLikes()
    {
        var gamerCode = profileGamercode.text;
        var form = new WWWForm();
        form.AddField("gamercode", gamerCode);
        var request = new WWW("http://localhost/sqlconnect/memberprofile.php", form);
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
