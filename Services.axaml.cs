using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using dem03.Context;
using Avalonia.Interactivity;
using System.Linq;
using dem03.Helper;
using dem03.Models;
using dem03;
using Castle.Components.DictionaryAdapter.Xml;
using Avalonia.Media.Imaging;
using Avalonia.Media;
using System.Security.Cryptography.X509Certificates;

namespace dem03;

public partial class Services : Window
{
    public List<Uslugi> uslugis = new List<Uslugi>();
    private TextBlock recordCountText;
    public Services()
    {
        InitializeComponent();
        BoxOne.SelectionChanged += BoxOne_SelectionChanged;
        BoxTwo.SelectionChanged += BoxTwo_SelectionChanged;
        recordCountText = this.FindControl<TextBlock>("RecordCountText");
        Lista();


    }




    public void Lista()
    {
        uslugis = Helper.Helper.Database.Uslugis.ToList();

        if (BoxOne.SelectedIndex == 1)
        {
            uslugis = uslugis.OrderBy(x => x.Cost).ToList();

        }
        else if (BoxOne.SelectedIndex == 2)
        {
            uslugis = uslugis.OrderByDescending(x => x.Cost).ToList();

        }
        if (BoxTwo.SelectedIndex == 1)
        {
            uslugis = uslugis.Where(x => x.Discount >= 0 & x.Discount <= 5).ToList();
        }
        else if (BoxTwo.SelectedIndex == 2)
        {
            uslugis = uslugis.Where(x => x.Discount >= 5 & x.Discount <= 14).ToList();
        }
        else if (BoxTwo.SelectedIndex == 3)
        {
            uslugis = uslugis.Where(x => x.Discount >= 15 & x.Discount <= 29).ToList();
        }

        else if (BoxTwo.SelectedIndex == 4)
        {
            uslugis = uslugis.Where(x => x.Discount >= 30 & x.Discount <= 69).ToList();
        }
        else if (BoxTwo.SelectedIndex == 5)
        {
            uslugis = uslugis.Where(x => x.Discount >= 70 & x.Discount <= 100).ToList();
        }



        var SearchTexxt = (SearchVoid.Text ?? "").Split(' ');
        uslugis = uslugis.Where(z => SearchTexxt.All(temp => z.Name.Contains(temp))).ToList();


        Listbox_MainWindow.ItemsSource = uslugis;

        UpdateRecordCount(uslugis.Count, Helper.Helper.Database.Uslugis.Count());



    }


    private void UpdateRecordCount(int filteredCount, int totalCount)
    {
        RecordCountText.Text = $"Записей выведено: {filteredCount} из {totalCount}";

    }
    private void BoxOne_SelectionChanged(object? sender, SelectionChangedEventArgs e) => Lista();
    private void BoxTwo_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        Lista();
    }

    private void TextBox_TextChanged(object? sender, Avalonia.Controls.TextChangedEventArgs e)
    {
        Lista();
    }
    private void MenuItem_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var redact = Listbox_MainWindow.SelectedItem as Uslugi;

        AddEditWindow redakt = new (redact);
        redakt.ShowDialog(this);
        redakt.Closed += (a, sd) => Lista();
    }

    private void MenuItem_Delete_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var selectedProduct = Listbox_MainWindow.SelectedItem as Uslugi;

        if (selectedProduct != null)
        {
            Helper.Helper.Database.Uslugis.Remove(selectedProduct);
            Helper.Helper.Database.SaveChanges();

            Lista();
        }

    }

    private void ListBox_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
    {
        new AddEditWindow().Show(); Close();
    }



    private void Button_EditClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var newUslugi = new Uslugi();
        var AddEditWindow = new AddEditWindow(newUslugi);
        AddEditWindow.ShowDialog(this);

    }






}
