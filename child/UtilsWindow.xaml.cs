using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UtilApp
{
    public partial class UtilsWindow : Window
    {
        public UtilsWindow()
        {
            InitializeComponent();
        }

        private void IconPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            // 展开图标栏
            IconPanel.Width = 200; // 展开宽度
        }

        private void IconPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            // 收回图标栏
            IconPanel.Width = 100; // 收回宽度
        }

        private void Icon1_Click(object sender, MouseButtonEventArgs e)
        {
            ShowSubMenu("图标1");
        }

        private void Icon2_Click(object sender, MouseButtonEventArgs e)
        {
            ShowSubMenu("图标2");
        }

        private void Icon3_Click(object sender, MouseButtonEventArgs e)
        {
            ShowSubMenu("图标3");
        }

        private void Icon4_Click(object sender, MouseButtonEventArgs e)
        {
            ShowSubMenu("图标4");
        }

        private void ShowSubMenu(string iconName)
        {
            // 清空功能区
            FunctionArea.Children.Clear();

            // 创建下级目录
            StackPanel subMenu = new StackPanel();
            subMenu.Margin = new Thickness(10);

            // 添加下级目录项
            for (int i = 1; i <= 2; i++)
            {
                Button subMenuButton = new Button
                {
                    Content = $"{iconName} - 子菜单{i}",
                    Margin = new Thickness(0, 5, 0, 0)
                };
                subMenuButton.Click += (s, e) => ShowFunction(iconName, i);
                subMenu.Children.Add(subMenuButton);
            }

            FunctionArea.Children.Add(subMenu);
        }

        private void ShowFunction(string iconName, int subMenuIndex)
        {
            // 更新功能区内容
            FunctionArea.Children.Clear();
            FunctionArea.Children.Add(new TextBlock
            {
                Text = $"{iconName} - 功能 {subMenuIndex}",
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            });
        }
    }
}