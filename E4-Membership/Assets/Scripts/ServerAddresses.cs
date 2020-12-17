public static class ServerAddresses
{
    private const bool LocalServer = false;
    
    private const string LocalLoginAddress = "http://localhost/backend/login.php";
    private const string LocalRegisterPageAddress = "http://localhost/backend/register.php";
    private const string LocalMembersPageAddress = "http://localhost/backend/memberspage.php";
    private const string LocalMemberProfileAddress = "http://localhost/backend/memberprofile.php";
    private const string LocalAddUserLikesAddress = "http://localhost/backend/addnewuserlike.php";

    private const string HostServerLoginAddress = "http://e4membership.000webhostapp.com/backend/backend/login.php";
    private const string HostServerRegisterPageAddress = "http://e4membership.000webhostapp.com/backend/backend/register.php";
    private const string HostServerMembersPageAddress = "http://e4membership.000webhostapp.com/backend/backend/memberspage.php";
    private const string HostServerMemberProfileAddress = "http://e4membership.000webhostapp.com/backend/backend/memberprofile.php";
    private const string HostServerAddUserLikesAddress = "http://e4membership.000webhostapp.com/backend/backend/addnewuserlike.php";
    
    public static string LoginAddress => LocalServer ? LocalLoginAddress : HostServerLoginAddress;
    public static string RegisterPageAddress => LocalServer ? LocalRegisterPageAddress : HostServerRegisterPageAddress;
    public static string MembersPageAddress => LocalServer ? LocalMembersPageAddress : HostServerMembersPageAddress;
    public static string MemberProfileAddress => LocalServer ? LocalMemberProfileAddress : HostServerMemberProfileAddress;
    public static string AddUserLikesAddress => LocalServer ? LocalAddUserLikesAddress : HostServerAddUserLikesAddress;
}
