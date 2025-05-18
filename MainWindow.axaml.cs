using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using demo3103.Context;
using Avalonia.Interactivity;
using System.Linq;
using demo3103.Models;
using demo2603;
using Castle.Components.DictionaryAdapter.Xml;
using Avalonia.Media.Imaging;
using Avalonia.Media;
using System.Security.Cryptography.X509Certificates;
using demo2603.Helper;

namespace demo3103;

public partial class MainWindow : Window
{
    public List<Partner> partners = Helper.Database.Partners.ToList();
    public MainWindow()
    {
        InitializeComponent();
        Listbox_Partner.ItemsSource = partners;
        ProdList();
    }
   
    public void ProdList()
    {
       partners = Helper.Database.Partners.ToList();
    }
    private void MenuItem_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var redactirovanie = Listbox_Partner.SelectedItem as Partner;

        AddEditWindow redakt = new(redactirovanie);
        redakt.ShowDialog(this);
        redakt.Closed += (a, sd) => ProdList();
    }
   
}


