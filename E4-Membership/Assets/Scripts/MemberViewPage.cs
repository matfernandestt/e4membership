using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MemberViewPage : MonoBehaviour
{
    public ProfileInfoPage profileInfoPage;
    
    [SerializeField] private Button refreshButton;
    [SerializeField] private Transform scrollContents;
    [SerializeField] private GameObject scrollbarLock;

    [SerializeField] private MemberInList memberButton;

    private void Awake()
    {
        refreshButton.onClick.AddListener(RefreshList);
        scrollbarLock.SetActive(false);
    }

    public void RefreshList()
    {
        scrollbarLock.SetActive(true);
        refreshButton.interactable = false;
        StartCoroutine(RefreshingList());
    }

    private IEnumerator RefreshingList()
    {
        var request = new WWW("http://localhost/sqlconnect/memberspage.php");
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
