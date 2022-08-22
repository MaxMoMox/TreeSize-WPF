using System;
using System.Globalization;
using System.Windows;
using TreeSize.Views.Windows;

namespace TreeSize.StartUp;

public class StartUp
{
    [STAThread]
    private static void Main()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

        var app = new Application();

        app.DispatcherUnhandledException += (sender, e) =>
        {
            MessageBox.Show("Something went wrong!\n" +
                            $"{e.Exception.Message}");
            e.Handled = true;

            app.Shutdown();
        };

        var mainWindow = new MainWindow();

        app.Run(mainWindow);
    }
}