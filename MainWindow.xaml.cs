using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace UtilApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // 左键是否按下
        private bool isDragging = false;
        // 鼠标点击位置
        private Point clickPosition;
        // TODO 拖尾相关(每trailNum次生成一个拖尾)
        private Point lastMousePosition;
        private int trailCount = 0;
        private int trailNum = 5;
        // TODO 吸附在屏幕边缘
        // TODO 系统托盘
        // TODO 代码抽象分层
        // TODO 桌面宠物功能
        // 动画是否正在播放（按钮组和悬浮球）
        private bool isAnimationPlaying = false;
        private bool utilsButtonPlaying = false;
        private bool urlButtonPlaying = false;
        private bool otherButtonPlaying = false;
        private bool settingButtonPlaying = false;
        // 新页面
        private UtilsWindow utilsWindow = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UtilButton_Click(object sender, RoutedEventArgs e)
        {
            if (utilsWindow == null || !utilsWindow.IsLoaded)
            {
                // 如果没有打开的新页面或者新页面已经被关闭，则创建并显示新页面
                utilsWindow = new UtilsWindow();
                utilsWindow.Closed += OnNewPageClosed; // 订阅新页面的关闭事件
                utilsWindow.Show();
                // 设置新页面的位置为屏幕中央
                SetWindowToCenter(utilsWindow);
                this.Hide(); // 隐藏主窗口
            }
        }

        private void SetWindowToCenter(Window window)
        {
            // 获取屏幕的工作区域（除去任务栏等）
            double screenWidth = SystemParameters.WorkArea.Width;
            double screenHeight = SystemParameters.WorkArea.Height;

            // 计算窗口的左边和上边位置，使其位于屏幕中央
            double left = (screenWidth / 2) - (window.Width / 2);
            double top = (screenHeight / 2) - (window.Height / 2);

            window.WindowStartupLocation = WindowStartupLocation.Manual; // 手动设置位置
            window.Left = left;
            window.Top = top;
        }

        private void OnNewPageClosed(object sender, EventArgs e)
        {
            // 当新页面关闭时重新显示主窗口
            this.Show();
            utilsWindow = null; // 清除新页面的引用
        }

        private void BallIcon_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point mousePos = e.GetPosition(this);
                this.Left += mousePos.X - clickPosition.X;
                this.Top += mousePos.Y - clickPosition.Y;
                trailCount++;
                // 拖尾处理
                //if (trailCount % trailNum == 0)
                //{
                //    trailCount = 0;
                //    AddTrail(mousePos);
                //    lastMousePosition = mousePos;
                //}
            }
            else
            {
                ButtonGroup.Visibility = Visibility.Visible; // 显示按钮组
                AnimationPlaying(ref isAnimationPlaying, false, "ScaleUpAnimation");
            }
        }

        private void BallIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationPlaying(ref isAnimationPlaying, true, "ScaleDownAnimation");
        }

        // 鼠标按下悬浮球
        private void BallIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ButtonGroup.Visibility = Visibility.Collapsed; // 隐藏按钮组
            isDragging = true;
            clickPosition = e.GetPosition(this);
            Mouse.Capture(BallIcon);
        }

        // 鼠标松开悬浮球
        private void BallIcon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            Mouse.Capture(null);
            ButtonGroup.Visibility = Visibility.Visible; // 显示按钮组
        }

        // 按钮组离开事件
        private void ButtonGroup_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonGroup.Visibility = Visibility.Collapsed; // 隐藏按钮组
        }

        // 工具栏按钮缩放动画
        private void UtilsButton_MouseMove(object sender, MouseEventArgs e)
        {
            AnimationPlaying(ref utilsButtonPlaying, false, "UtilsButtonScaleUpAnimation");
        }
        private void UtilsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationPlaying(ref utilsButtonPlaying, true, "UtilsButtonScaleDownAnimation");
        }

        // 导航栏按钮缩放动画
        private void UrlButton_MouseMove(object sender, MouseEventArgs e)
        {
            AnimationPlaying(ref urlButtonPlaying, false, "UrlButtonScaleUpAnimation");
        }
        private void UrlButton_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationPlaying(ref urlButtonPlaying, true, "UrlButtonScaleDownAnimation");
        }

        // 其他按钮缩放动画
        private void OtherButton_MouseMove(object sender, MouseEventArgs e)
        {
            AnimationPlaying(ref otherButtonPlaying, false, "OtherButtonScaleUpAnimation");
        }
        private void OtherButton_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationPlaying(ref otherButtonPlaying, true, "OtherButtonScaleDownAnimation");
        }

        // 设置按钮缩放动画
        private void SettingButton_MouseMove(object sender, MouseEventArgs e)
        {
            AnimationPlaying(ref settingButtonPlaying, false, "SettingButtonScaleUpAnimation");
        }
        private void SettingButton_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationPlaying(ref settingButtonPlaying, true, "SettingButtonScaleDownAnimation");
        }

        // 带布尔标记的动画播放
        private void AnimationPlaying(ref bool flag, bool exeFlag, string animation)
        {
            if (flag == exeFlag)
            {
                // 启动放大动画
                Storyboard scaleUpAnimation = (Storyboard)this.Resources[animation];
                scaleUpAnimation.Begin();
                flag = !flag;
            }
        }

        private void AddTrail(Point position)
        {
            // 根据方向调整拖尾生成的位置
            double offsetX = position.X - lastMousePosition.X;
            double offsetY = position.Y - lastMousePosition.Y;
            double distance = Math.Sqrt(offsetX * offsetX + offsetY * offsetY);
            double angle = Math.Atan2(offsetY, offsetX) * (180 / Math.PI); // 角度转换为度数

            // 根据角度调整偏移量
            double xOffset = Math.Cos((angle + 90) * (Math.PI / 180)) * 20; // 偏移10像素
            double yOffset = Math.Sin((angle + 90) * (Math.PI / 180)) * 20;

            Image trailImage = new Image()
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/image/2.png")),
                Width = 20,
                Height = 20,
                Opacity = 0.8, // 初始透明度稍高一点
                RenderTransformOrigin = new Point(0.5, 0.5),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(position.X - xOffset - 35, position.Y - yOffset - 35, 0, 0) // 调整Margin使图像中心位于鼠标指针处
            };

            ScaleTransform scaleTransform = new ScaleTransform(0.8, 0.8); // 初始比例稍微大一点
            trailImage.RenderTransform = scaleTransform;

            BallContainer.Children.Add(trailImage);

            DoubleAnimation opacityAnimation = new DoubleAnimation(0.8, 0, TimeSpan.FromSeconds(0.5));
            DoubleAnimation scaleXAnimation = new DoubleAnimation(0.8, 1.2, TimeSpan.FromSeconds(0.5)); // 放大效果
            DoubleAnimation scaleYAnimation = new DoubleAnimation(0.8, 1.2, TimeSpan.FromSeconds(0.5));

            trailImage.BeginAnimation(Image.OpacityProperty, opacityAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);

            opacityAnimation.Completed += (s, ev) => BallContainer.Children.Remove(trailImage);
        }
    }
}