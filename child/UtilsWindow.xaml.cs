using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UtilApp.Common;

namespace UtilApp
{
    public partial class UtilsWindow : Window
    {
public UtilsWindow()
{
    InitializeComponent();
    DataContext = new MainViewModel(); // 确保MainViewModel是你定义的视图模型类
}

private void TreeView_Selected(object sender, RoutedPropertyChangedEventArgs<object> e)
{
    var selectedItem = e.NewValue as SubNavigationItem;
    if (selectedItem != null && selectedItem.PageType != null)
    {
        // MainFrame.Navigate(new Uri("SwapPage.xaml", UriKind.Relative));
        MainFrame.Navigate(Activator.CreateInstance(selectedItem.PageType) as Page);
        
    }
}
    }
}