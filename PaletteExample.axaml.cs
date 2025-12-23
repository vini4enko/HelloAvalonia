using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Text;

namespace HelloAvalonia;

public partial class PaletteExample : Avalonia.Controls.Window
{
    public PaletteExample()
    {
        InitializeComponent();
        SystemDecorations = SystemDecorations.None;
        Title = "Счётчик примитивов";
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        // Словарь для подсчета
        Dictionary<string, int> counts = Examples.GetCounts();
        // Показываем результаты
        StringBuilder sb = new StringBuilder();
        //Добавляем строки с результатами
        foreach (var item in counts)
        {
            sb.Append(item.Key.PadRight(27));
            sb.AppendLine(item.Value.ToString().PadLeft(3));
        }
        table.Text = sb.ToString();
        // Итоговая строка
        int tt = counts.Values.Sum();
        total.Text = "Всего:".PadRight(25) + tt.ToString();
    }
}