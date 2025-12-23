using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using HostMgd.EditorInput;
using System.Security.Cryptography;

namespace HelloAvalonia;

public partial class WindowExample : Window
{
    public WindowExample()
    {
        InitializeComponent();
    }
    // Обработчик нажатия на кнопку
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        txtBox.Text = Examples.GetPoint();
    }
}