using UnityEngine;

public class PageManager : MonoBehaviour
{
    [SerializeField] private Login loginPage;
    [SerializeField] private UserRegistration registerPage;
    [SerializeField] private MemberViewPage memberViewPage;

    [SerializeField] private Animator loginPageAnim;
    [SerializeField] private Animator registerPageAnim;
    [SerializeField] private Animator membersViewPageAnim;
    
    private static readonly int CloseTag = Animator.StringToHash("Close");
    private static readonly int OpenTag = Animator.StringToHash("Open");

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
        loginPageAnim.SetTrigger(OpenTag);
        loginPage.EnableLogin();
    }

    private void OpenMemberViewPage()
    {
        DisableAllPages();
        memberViewPage.gameObject.SetActive(true);
        membersViewPageAnim.SetTrigger(OpenTag);
        memberViewPage.RefreshList();
        memberViewPage.OnOpenWindow();
    }

    private void OpenRegisterPage()
    {
        DisableAllPages();
        registerPage.gameObject.SetActive(true);
        registerPageAnim.SetTrigger(OpenTag);
    }

    private void DisableAllPages()
    {
        //loginPage.gameObject.SetActive(false);
        //registerPage.gameObject.SetActive(false);
        //memberViewPage.gameObject.SetActive(false);
        
        ResetTriggers();
        loginPageAnim.SetTrigger(CloseTag);
        registerPageAnim.SetTrigger(CloseTag);
        membersViewPageAnim.SetTrigger(CloseTag);
    }

    private void ResetTriggers()
    {
        loginPageAnim.ResetTrigger(CloseTag);
        registerPageAnim.ResetTrigger(CloseTag);
        membersViewPageAnim.ResetTrigger(CloseTag);
        loginPageAnim.ResetTrigger(OpenTag);
        registerPageAnim.ResetTrigger(OpenTag);
        membersViewPageAnim.ResetTrigger(OpenTag);
    }
}