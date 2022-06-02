namespace LawOfficeDesktopApp.Services
{
    /// <summary>
    /// Verifies the password. The default password is 1234.
    /// </summary>
    public class Guard : IGuard<string>
    {
        private const string DefaultEmployeePassword = "1234";

        public string EmployeePassword { get; set; } = DefaultEmployeePassword;
        public bool Verify(string target)
        {
            return target == EmployeePassword;
        }
    }
}
