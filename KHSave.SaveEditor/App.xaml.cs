using KHSave.SaveEditor.Ff7Remake.Data;
using KHSave.SaveEditor.Interfaces;
using KHSave.SaveEditor.Services;
using KHSave.SaveEditor.Views;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using Unity;

namespace KHSave.SaveEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private class ApplicationStartup : IApplicationStartup
        {
            public ApplicationStartup(string[] args)
            {
                StartupFileName = args.FirstOrDefault();
#if DEBUG
                StartupFileName = StartupFileName ?? @"D:\Repository\KH3SaveEditor\KHSave.Tests\Saves\kh2fm.bin";
#endif
            }

#if DEBUG
            public bool IsDebugging => Debugger.IsAttached;
#else
            public bool IsDebugging => false;
#endif

            public string StartupFileName { get; }
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);
                Initialize(e);
            }
            catch (Exception ex)
            {
                CaptureException(ex);
            }
        }

        private void Initialize(StartupEventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            DispatcherUnhandledException += (sender, args) =>
            {
                CaptureException(args.Exception);
                args.Handled = true;
            };

            ItemsPreset.LazyInitialize();
            BgmPreset.LazyInitialize();

            IUnityContainer container = new UnityContainer()
                .AddExtension(new Diagnostic())
                .RegisterSingleton<IWindowManager, WindowManager>()
                .RegisterSingleton<IFileDialogManager, FileDialogManager>()
                .RegisterInstance<IApplicationStartup>(new ApplicationStartup(e.Args))
                .RegisterSingleton<IAlertMessage, AlertMessage>()
                .RegisterSingleton<IUpdater, UpdaterService>()
                .RegisterSingleton<IAppIdentity, DesktopAppIdentity>()
                ;

            container.Resolve<MainWindow>().Show();
        }

        private static void CaptureException(Exception ex)
        {
            ReporterService.Instance.SendCrashReport(ex);
            MessageBox.Show(
                $"An unhandled error has occurred:\n{ex.Message}\n\n{ex.StackTrace}",
                "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
