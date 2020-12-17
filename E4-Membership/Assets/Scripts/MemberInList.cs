using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MemberInList : MonoBehaviour
{
    [SerializeField] private RawImage imageSlot;
    [SerializeField] private TextMeshProUGUI textSlot;

    [SerializeField] private Button slotButton;

    private string memberId;
    private string memberGamercode;
    private string memberUsername;
    private string memberPhotoUrl;
    private Texture2D memberPhoto;

    private Coroutine photoParseCoroutine;

    public void SetupPlayer(string id, string gamerCode, string username, string photo, MemberViewPage memberViewPage)
    {
        textSlot.text = username;
        if(photoParseCoroutine != null)
            StopCoroutine(photoParseCoroutine);
        photoParseCoroutine = StartCoroutine(ParsePhoto(photo));

        memberId = id;
        memberGamercode = gamerCode;
        memberUsername = username;
        
        slotButton.onClick.AddListener(() => memberViewPage.profileInfoPage.OpenProfilePage(memberPhotoUrl, memberPhoto, memberUsername, memberGamercode));
    }

    private IEnumerator ParsePhoto(string url)
    {
        memberPhotoUrl = url;
        var wwwPhoto = new WWW(url);
        yield return wwwPhoto;
        imageSlot.texture = wwwPhoto.texture;
        memberPhoto = wwwPhoto.texture;
        wwwPhoto.Dispose();
    }
}