using System.Windows;
using System.Windows.Controls;

namespace UtilApp
{
    public partial class StringStatPage : Page
    {
        public StringStatPage()
        {
            InitializeComponent();
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string inputText = InputTextBox.Text;

            // 统计字符数
            int totalCharacters = inputText.Length;
            int chineseCharacters = 0;
            int englishCharacters = 0;

            foreach (char c in inputText)
            {
                if (char.IsLetter(c))
                {
                    if (IsChinese(c))
                    {
                        chineseCharacters++;
                    }
                    else
                    {
                        englishCharacters++;
                    }
                }
            }

            // 更新统计信息
            CharacterCountText.Text = $"字符总数: {totalCharacters} 个";
            ChineseCharacterCountText.Text = $"中文字符总数: {chineseCharacters} 个";
            EnglishCharacterCountText.Text = $"英文字符总数: {englishCharacters} 个";
            TotalLineCountText.Text = $"总行数: {inputText.Split('\n').Length} 行";
            ParagraphCountText.Text = $"段落数: {inputText.Split(new[] { "\n\n" }, StringSplitOptions.None).Length} 段";
        }

        private bool IsChinese(char c)
        {
            return c >= 0x4E00 && c <= 0x9FA5; // 汉字的Unicode范围
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            // 清除按钮的功能
            InputTextBox.Clear();
        }
    }
}