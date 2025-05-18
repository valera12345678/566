using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Linq;

namespace dem03;


public partial class MainWindow : Window
{
    public MainWindow()

    {
        InitializeComponent();
    }

    private void LogClick(object? sender, RoutedEventArgs e)
    {
       
        var Pass = Password.Text;

        var user = Helper.Helper.Database.Admins.FirstOrDefault(x => x.Password == Pass);

        if (user != null)
        {

            var window = new Services();
            window.Show();
            Close();
        }

    }

}