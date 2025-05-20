using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // ЛОГИРОВАНИЕ
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace smarrtCalculator
{
    public partial class programist : Form
    {
        // ЛОГИРОВАНИЕ: переменные для лога
        private string sessionLogFilePath;
        private DateTime sessionStartTime;

        public programist()
        {
            InitializeComponent();
            ApplyTheme();
            Helper.SetFontSize(this, AppSettings.FontSize);

            //инициализация лога при запуске формы
            sessionStartTime = DateTime.Now;
            string fileName = $"SessionLog_{sessionStartTime:yyyy-MM-dd_HH-mm-ss}.txt";
            string projectDir = AppDomain.CurrentDomain.BaseDirectory;
            sessionLogFilePath = Path.Combine(projectDir, fileName);
            File.AppendAllText(sessionLogFilePath, $"Сессия начата: {sessionStartTime}\r\n\r\n");
        }

        private List<string> history = new List<string>();
        long a, b, c;
        string num1, num2, sum;

        int count;
        bool znak = true;
        string operation;
        bool flagHEX, flagDEC, flagOCT, flagBIN = false;
        short numberSystem = 10;

        bool isOperatorClicked = false;

        // ЛОГИРОВАНИЕ: функция для записи действия в лог
        private void LogAction(string action)
        {
            string logEntry = $"[{DateTime.Now:HH:mm:ss}] {action}\r\n";
            File.AppendAllText(sessionLogFilePath, logEntry);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 2;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 3;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 4;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 5;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 6;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 7;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 8;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 9;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            isOperatorClicked = false;
            calculate();
            Operator_Click(sender, e);
        }
        private void button14_Click(object sender, EventArgs e)
        {
            isOperatorClicked = false;
            calculate();
            Operator_Click(sender, e);
        }
        private void button15_Click(object sender, EventArgs e)
        {
            isOperatorClicked = false;
            calculate();
            Operator_Click(sender, e);
        }
        private void button16_Click(object sender, EventArgs e)
        {
            isOperatorClicked = false;
            calculate();
            Operator_Click(sender, e);
        }

        private void programist_Load(object sender, EventArgs e)
        {
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 0;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            calculate();
            label5.Text = "";
            count = 0;
            if (a != 0)
            {
                textBox1.Text = Convert.ToString(a, numberSystem).ToUpper();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + ",";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int lenght = textBox1.Text.Length - 1;
            string text = textBox1.Text;
            textBox1.Clear();
            for (int i = 0; i < lenght; i++)
            {
                textBox1.Text = textBox1.Text + text[i];
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (znak == true)
            {
                textBox1.Text = "-" + textBox1.Text;
                znak = false;
                LogAction("Смена знака на отрицательный"); // ЛОГИРОВАНИЕ
            }
            else if (znak == false)
            {
                textBox1.Text = textBox1.Text.Replace("-", "");
                znak = true;
                LogAction("Смена знака на положительный"); // ЛОГИРОВАНИЕ
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            label5.Text = "";
            a = 0;
            b = 0;
            c = 0;
            LogAction("Очищен калькулятор"); // ЛОГИРОВАНИЕ
        }

        private void button21_Click(object sender, EventArgs e)
        {
            hiddenForm2 hiddenForm2 = new hiddenForm2(history, this);
            hiddenForm2.ShowDialog();
        }

        private void Operator_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                if (count == 0)
                {
                    a = Convert.ToInt64(textBox1.Text, numberSystem);
                }
            }
            textBox1.Clear();
            Button btn = sender as Button;
            string displayAForLabel = Convert.ToString(a, numberSystem).ToUpper();

            switch (btn.Text)
            {
                case "+": count = 1; label5.Text = $"{displayAForLabel} +"; break;
                case "-": count = 2; label5.Text = $"{displayAForLabel} -"; break;
                case "*": count = 3; label5.Text = $"{displayAForLabel} *"; break;
                case "/": count = 4; label5.Text = $"{displayAForLabel} /"; break;
            }
            isOperatorClicked = true;
            znak = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!isOperatorClicked)
            {
                label5.Text = textBox1.Text;
            }
            try
            {
                long i = Convert.ToInt32(textBox1.Text, numberSystem);
                label1.Text = Convert.ToString(i, 16).ToUpper();
                label2.Text = Convert.ToString(i, 10);
                label3.Text = Convert.ToString(i, 8);
                label4.Text = Convert.ToString(i, 2);
            }
            catch (FormatException)
            {
                ClearLabel();
            }
            catch (ArgumentOutOfRangeException)
            {
                ClearLabel();
            }
            catch (OverflowException)
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
            catch (ArgumentException)
            {

            }
        }

        private void buttonA_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "A";
        }
        private void buttonB_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "B";
        }
        private void buttonC_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "C";
        }
        private void buttonD_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "D";
        }
        private void buttonE_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "E";
        }

        private void buttonHEX_Click(object sender, EventArgs e)
        {
            buttonA.Enabled = true; button1.Enabled = true;
            buttonB.Enabled = true; button0.Enabled = true;
            buttonC.Enabled = true; button2.Enabled = true;
            buttonD.Enabled = true; button3.Enabled = true;
            buttonE.Enabled = true; button4.Enabled = true;
            buttonF.Enabled = true; button5.Enabled = true;
            button6.Enabled = true; button7.Enabled = true;
            button8.Enabled = true; button9.Enabled = true;

            flagHEX = true;
            flagDEC = false; flagOCT = false; flagBIN = false;

            numberSystem = 16;
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = "";
            }
            else
            {
                textBox1.Text = label1.Text;
            }
            LogAction("Смена системы счисления на HEX"); // ЛОГИРОВАНИЕ
        }
        private void buttonBIN_Click(object sender, EventArgs e)
        {
            buttonA.Enabled = false; button1.Enabled = true;
            buttonB.Enabled = false; button0.Enabled = true;
            buttonC.Enabled = false; button2.Enabled = false;
            buttonD.Enabled = false; button3.Enabled = false;
            buttonE.Enabled = false; button4.Enabled = false;
            buttonF.Enabled = false; button5.Enabled = false;
            button6.Enabled = false; button7.Enabled = false;
            button8.Enabled = false; button9.Enabled = false;

            flagBIN = true;
            flagDEC = false; flagHEX = false; flagOCT = false;

            numberSystem = 2;
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = "";
            }
            else
            {
                textBox1.Text = label4.Text;
            }
            LogAction("Смена системы счисления на BIN"); // ЛОГИРОВАНИЕ
        }

        private void buttonDEC_Click(object sender, EventArgs e)
        {
            buttonA.Enabled = false; button1.Enabled = true;
            buttonB.Enabled = false; button0.Enabled = true;
            buttonC.Enabled = false; button2.Enabled = true;
            buttonD.Enabled = false; button3.Enabled = true;
            buttonE.Enabled = false; button4.Enabled = true;
            buttonF.Enabled = false; button5.Enabled = true;
            button6.Enabled = true; button7.Enabled = true;
            button8.Enabled = true; button9.Enabled = true;

            flagDEC = true;
            flagBIN = false; flagHEX = false; flagOCT = false;

            numberSystem = 10;
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = "";
            }
            else
            {
                textBox1.Text = label2.Text;
            }
            LogAction("Смена системы счисления на DEC"); // ЛОГИРОВАНИЕ
        }

        private void buttonOCT_Click(object sender, EventArgs e)
        {
            buttonA.Enabled = false; button1.Enabled = true;
            buttonB.Enabled = false; button0.Enabled = true;
            buttonC.Enabled = false; button2.Enabled = true;
            buttonD.Enabled = false; button3.Enabled = true;
            buttonE.Enabled = false; button4.Enabled = true;
            buttonF.Enabled = false; button5.Enabled = true;
            button6.Enabled = true; button7.Enabled = true;
            button8.Enabled = false; button9.Enabled = false;

            flagOCT = true;
            flagDEC = false; flagHEX = false; flagBIN = false;

            numberSystem = 8;
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = "";
            }
            else
            {
                textBox1.Text = label3.Text;
            }
            LogAction("Смена системы счисления на OCT"); // ЛОГИРОВАНИЕ
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            parametrs parametrs = new parametrs();
            parametrs.Show();
            this.Hide();
        }

        private void buttonF_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "F";
        }

        private void calculate()
        {
            try
            {
                b = Convert.ToInt64(textBox1.Text, numberSystem);
                if (count == 4 && b == 0)
                {
                    MessageBox.Show("Ошибка, на 0 делить нельзя");
                    a = 0;
                    b = 0;
                    count = 0;
                    textBox1.Text = "";
                    label5.Text = "";
                    znak = true;
                    return;
                }
                switch (count)
                {
                    case 1:
                        c = a + b;
                        Converter();
                        break;
                    case 2:
                        c = a - b;
                        Converter();
                        break;
                    case 3:
                        c = a * b;
                        Converter();
                        break;
                    case 4:
                        if (b == 0)
                        {
                            MessageBox.Show("Ошибка, на 0 делить нельзя");
                            a = 0;
                            b = 0;
                            count = 0;
                            textBox1.Text = "";
                            label5.Text = "";
                            znak = true;
                            return;
                        }
                        c = a / b;
                        Converter();
                        break;
                    default:
                        break;
                }
            }
            catch (FormatException)
            {
                textBox1.Text = Convert.ToString(a, numberSystem).ToUpper();
                a = 0;
                return;
            }
            catch (ArgumentOutOfRangeException)
            {

            }

            try
            {
                long i = Convert.ToInt64(textBox1.Text, numberSystem);
                label1.Text = Convert.ToString(i, 16).ToUpper();
                label2.Text = Convert.ToString(i, 10);
                label3.Text = Convert.ToString(i, 8);
                label4.Text = Convert.ToString(i, 2);
            }
            catch (FormatException)
            {
                ClearLabel();
            }
            catch (ArgumentOutOfRangeException)
            {

            }
            isOperatorClicked = false;
        }

        public void Converter()
        {
            textBox1.Text = Convert.ToString(c, numberSystem).ToUpper();

            string op;
            switch (count)
            {
                case 1: op = "+"; break;
                case 2: op = "-"; break;
                case 3: op = "*"; break;
                case 4: op = "/"; break;
                default: op = "?"; break;
            }
            operation = $"{Convert.ToString(a, numberSystem).ToUpper()} {op} {Convert.ToString(b, numberSystem).ToUpper()} = {Convert.ToString(c, numberSystem).ToUpper()}";
            history.Add(operation);
            a = c;

            LogAction(operation); // ЛОГИРОВАНИЕ: запись вычисления в лог
        }

        public void ClearHistory()
        {
            history.Clear();
        }

        public void ClearLabel()
        {
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
        }
        private void ApplyTheme(Control control)
        {
            bool isDark = AppSettings.IsDarkTheme; // Сохраняем локально для удобства

            if (isDark)
            {
                control.BackColor = Color.FromArgb(45, 45, 48);
                control.ForeColor = Color.White;
            }
            else
            {
                control.BackColor = SystemColors.Control;
                control.ForeColor = Color.Black;
            }

            // Рекурсивно применяем ко всем дочерним контролам
            foreach (Control child in control.Controls)
            {
                ApplyTheme(child);
            }
        }

        public void ApplyTheme()
        {
            ApplyTheme(this);
        }
    }
}
