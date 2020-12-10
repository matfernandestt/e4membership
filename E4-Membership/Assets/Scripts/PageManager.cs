using UnityEngine;

public class PageManager : MonoBehaviour
{
    [SerializeField] private Login loginPage;
    [SerializeField] private UserRegistration registerPage;
    [SerializeField] private MemberViewPage memberViewPage;

    private void Awake()
    {
        loginPage.OnLoginSuccessfull += OpenMemberViewPage;
        loginPage.OnClickEnterAsGuest += OpenMemberViewPage;
        loginPage.OnClickRegister += OpenRegisterPage;

        registerPage.OnCompleteRegistration += OpenMemberViewPage;
        registerPage.OnClickGoBack += OpenLoginPage;

        memberViewPage.OnClickLoginButton += OpenLoginPage;
        memberViewPage.OnClickRegisterButton += OpenRegisterPage;
    }

    private void OnDestroy()
    {
        loginPage.OnLoginSuccessfull -= OpenMemberViewPage;
        loginPage.OnClickEnterAsGuest -= OpenMemberViewPage;
        loginPage.OnClickRegister -= OpenRegisterPage;
        
        registerPage.OnCompleteRegistration -= OpenMemberViewPage;
        registerPage.OnClickGoBack -= OpenLoginPage;
        
        memberViewPage.OnClickLoginButton -= OpenLoginPage;
        memberViewPage.OnClickRegisterButton -= OpenRegisterPage;
    }

    private void OpenLoginPage()
    {
        DisableAllPages();
        loginPage.gameObject.SetActive(true);
        loginPage.EnableLogin();
    }

    private void OpenMemberViewPage()
    {
        DisableAllPages();
        memberViewPage.gameObject.SetActive(true);
        
        memberViewPage.RefreshList();
    }

    private void OpenRegisterPage()
    {
        DisableAllPages();
        registerPage.gameObject.SetActive(true);
    }

    private void DisableAllPages()
    {
        loginPage.gameObject.SetActive(false);
        registerPage.gameObject.SetActive(false);
        memberViewPage.gameObject.SetActive(false);
    }
}
