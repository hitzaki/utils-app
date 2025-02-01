using System.Windows;
using System.Windows.Controls;

namespace UtilApp
{
    public partial class StringSwapPage : Page // 确保 Page 是正确的
    {
        private string previousText = string.Empty; // 用于记录上一次的文本
        private int operationCount = 0; // 操作记录计数
        private const int maxOperations = 10; // 最大操作记录数
        private string[] operationHistory = new string[maxOperations]; // 操作记录

        public StringSwapPage()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            // 记录当前文本以便回滚
            previousText = InputTextBox.Text;

            // 根据下拉框选择执行相应操作
            if (FunctionComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "替换":
                        ReplaceText();
                        break;
                    case "转义":
                        EscapeText();
                        break;
                    case "分割":
                        SplitText();
                        break;
                }
            }
        }

        private void ReplaceText()
        {
            string before = BeforeTextBox.Text;
            string after = AfterTextBox.Text;
            InputTextBox.Text = InputTextBox.Text.Replace(before, after);
            RecordOperation(InputTextBox.Text);
        }

        private void EscapeText()
        {
            // 示例：将 \n 转义为换行符
            InputTextBox.Text = InputTextBox.Text.Replace("\\n", "\n");
            RecordOperation(InputTextBox.Text);
        }

        private void SplitText()
        {
            string delimiter = DelimiterTextBox.Text;
            string[] parts = InputTextBox.Text.Split(new[] { delimiter }, StringSplitOptions.None);
            InputTextBox.Text = string.Join("\n", parts); // 用换行符显示分割结果
            RecordOperation(InputTextBox.Text);
        }

        private void RecordOperation(string newText)
        {
            if (operationCount < maxOperations)
            {
                operationHistory[operationCount++] = newText; // 记录操作
            }
            else
            {
                // 如果超过最大记录数，移除最旧的记录
                for (int i = 1; i < maxOperations; i++)
                {
                    operationHistory[i - 1] = operationHistory[i];
                }
                operationHistory[maxOperations - 1] = newText; // 添加新记录
            }
        }

        private void RollbackButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(previousText))
            {
                MessageBoxResult result = MessageBox.Show("确认要回滚到上一次状态吗？", "确认", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    InputTextBox.Text = previousText; // 回滚到上一次状态
                }
            }
        }

        private void FunctionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 根据选择的功能显示相应的操作区
            ReplaceArea.Visibility = Visibility.Collapsed;
            SplitArea.Visibility = Visibility.Collapsed;

            if (FunctionComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "替换":
                        ReplaceArea.Visibility = Visibility.Visible;
                        break;
                    case "分割":
                        SplitArea.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            // 复制功能
            Clipboard.SetText(InputTextBox.Text);
            MessageBox.Show("文本已复制到剪贴板");
        }
    }
}