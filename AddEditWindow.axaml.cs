using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using dem03.Models;

namespace dem03;

public partial class AddEditWindow : Window
{
    public Uslugi CurrentPartner;

    public AddEditWindow()
    {
    }

    public AddEditWindow(Uslugi newProduct)
    {
        InitializeComponent();
        CurrentPartner = new Uslugi();
    }

    private void Button_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close();
    }

    private void Button_Click_2(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        {
            Helper.Helper.Database.Uslugis.Update(CurrentPartner);
        }

        Helper.Helper.Database.SaveChanges();
        Close();
    }
}