using System.Diagnostics;
using System.Reflection;

namespace KHSave.SaveEditor.Services
{
    public class DesktopAppIdentity : IAppIdentity
    {
        private static Assembly _assembly;
        private static FileVersionInfo _fvi;

        public DesktopAppIdentity()
        {
            _assembly = Assembly.GetExecutingAssembly();
            _fvi = FileVersionInfo.GetVersionInfo(_assembly.Location);

            // https://docs.microsoft.com/en-us/windows/msix/desktop/desktop-to-uwp-behind-the-scenes#installation
            IsMicrosoftStore = _assembly.Location.Contains("/WindowsApps/");
        }

        public string Name => _fvi.ProductName;

        public string Version => _fvi.ProductVersion;

        public bool IsMicrosoftStore { get; }
    }
}
