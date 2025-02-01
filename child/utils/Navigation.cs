using System.Collections.ObjectModel;
using UtilApp.Common;
using UtilApp;

public class MainViewModel
{
    public ObservableCollection<NavigationItem> NavigationItems { get; set; }

    public MainViewModel()
    {
        // 初始化导航项
        NavigationItems = new ObservableCollection<NavigationItem>
        {
            new NavigationItem 
            { 
                Name = "字符串处理工具", 
                ImageUrl = "pack://application:,,,/image/2.png",
                SubItems = new List<SubNavigationItem>
                {
                    new SubNavigationItem { Name = "格式转换", PageType = typeof(StringSwapPage) },
                    new SubNavigationItem { Name = "字符串统计", PageType = typeof(StringStatPage) },
                    new SubNavigationItem { Name = "文本对比", PageType = typeof(StringSwapPage) },
                    new SubNavigationItem { Name = "正则表达式", PageType = typeof(StringSwapPage) },
                } 
            },
            new NavigationItem
            { 
                Name = "编解码工具", 
                ImageUrl = "pack://application:,,,/image/2.png",
                SubItems = new List<SubNavigationItem>
                {
                    new SubNavigationItem { Name = "token编解码", PageType = typeof(StringSwapPage) },
                    new SubNavigationItem { Name = "URL编解码", PageType = typeof(StringSwapPage) },
                } 
            },
            // 添加更多导航项...
        };
    }
}