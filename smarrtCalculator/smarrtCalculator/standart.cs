using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace smarrtCalculator
{
    public partial class standart : Form
    {
        private string sessionLogFilePath;
        private DateTime sessionStartTime;

        public standart()
        {
            InitializeComponent();
            ApplyTheme();
            Helper.SetFontSize(this, AppSettings.FontSize);

            sessionStartTime = DateTime.Now;
            string fileName = $"SessionLog_{sessionStartTime:yyyy-MM-dd_HH-mm-ss}.txt";
            string projectDir = AppDomain.CurrentDomain.BaseDirectory;
            sessionLogFilePath = Path.Combine(projectDir, fileName);
            File.AppendAllText(sessionLogFilePath, $"Сессия начата: {sessionStartTime}\r\n\r\n");
        }

        private List<string> history = new List<string>();
        float a, b, c;
        int count;
        bool znak = true;
        string operation;

        public void ClearHistory()
        {
            history.Clear();
        }
        private void standart_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 0;
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

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + ",";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Operator_Click(sender, e);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Operator_Click(sender, e);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Operator_Click(sender, e);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Operator_Click(sender, e);
        }

        private void LogAction(string action)
        {
            string logEntry = $"[{DateTime.Now:HH:mm:ss}] {action}\r\n";
            File.AppendAllText(sessionLogFilePath, logEntry);
        }

        private void Operator_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                b = float.Parse(textBox1.Text);
                if (count == 4 && b == 0)
                {
                    MessageBox.Show("Ошибка, на 0 делить нельзя");
                    a = 0;
                    b = 0;
                    count = 0;
                    textBox1.Text = "";
                    label1.Text = "";
                    znak = true;
                    LogAction("Попытка деления на 0");
                    return;
                }
                if (count != 0)
                {
                    switch (count)
                    {
                        case 1:
                            a = a + b;
                            operation = $"{a - b} + {b} = {a}";
                            history.Add(operation);
                            LogAction(operation);
                            break;
                        case 2:
                            a = a - b;
                            operation = $"{a + b} - {b} = {a}";
                            history.Add(operation);
                            LogAction(operation);
                            break;
                        case 3:
                            a = a * b;
                            operation = $"{a / b} * {b} = {a}";
                            history.Add(operation);
                            LogAction(operation);
                            break;
                        case 4:
                            a = a / b;
                            operation = $"{a * b} / {b} = {a}";
                            history.Add(operation);
                            LogAction(operation);
                            break;
                    }
                }
                else
                {
                    a = float.Parse(textBox1.Text);
                }
            }
            textBox1.Clear();
            Button btn = sender as Button;
            switch (btn.Text)
            {
                case "+": count = 1; label1.Text = a + "+"; LogAction($"Выбрана операция +, a={a}"); break;
                case "-": count = 2; label1.Text = a + "-"; LogAction($"Выбрана операция -, a={a}"); break;
                case "*": count = 3; label1.Text = a + "*"; LogAction($"Выбрана операция *, a={a}"); break;
                case "/": count = 4; label1.Text = a + "/"; LogAction($"Выбрана операция /, a={a}"); break;
            }
            znak = true;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            calculate();
            label1.Text = "";
            count = 0;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (znak == true)
            {
                textBox1.Text = "-" + textBox1.Text;
                znak = false;
                LogAction("Смена знака на отрицательный");
            }
            else if (znak == false)
            {
                textBox1.Text = textBox1.Text.Replace("-", "");
                znak = true;
                LogAction("Смена знака на положительный");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            label1.Text = "";
            a = 0;
            b = 0;
            c = 0;
            LogAction("Очищен калькулятор");
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

        private void button21_Click(object sender, EventArgs e)
        {
            hiddenForm hiddenForm = new hiddenForm(history, this);
            hiddenForm.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            parametrs parametrs = new parametrs();
            parametrs.Show();
            this.Hide();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            programist programist = new programist();
            this.Hide();
            programist.ShowDialog();
        }

        private void calculate()
        {
            try
            {
                b = float.Parse(textBox1.Text);
                if (count == 4 && b == 0)
                {
                    MessageBox.Show("Ошибка, на 0 делить нельзя");
                    a = 0;
                    b = 0;
                    count = 0;
                    textBox1.Text = "";
                    label1.Text = "";
                    znak = true;
                    LogAction("Попытка деления на 0");
                    return;
                }
                switch (count)
                {
                    case 1:
                        c = a + b;
                        textBox1.Text = c.ToString();
                        operation = $"{a} + {b} = {c}";
                        history.Add(operation);
                        LogAction(operation);
                        a = c;
                        break;
                    case 2:
                        c = a - b;
                        textBox1.Text = c.ToString();
                        operation = $"{a} - {b} = {c}";
                        history.Add(operation);
                        LogAction(operation);
                        a = c;
                        break;
                    case 3:
                        c = a * b;
                        textBox1.Text = c.ToString();
                        operation = $"{a} * {b} = {c}";
                        history.Add(operation);
                        LogAction(operation);
                        a = c;
                        break;
                    case 4:
                        c = a / b;
                        textBox1.Text = c.ToString();
                        operation = $"{a} / {b} = {c}";
                        history.Add(operation);
                        LogAction(operation);
                        a = c;
                        break;
                    default:
                        break;
                }
            }
            catch (FormatException)
            {
                textBox1.Text = a.ToString();
                a = 0;
                return;
            }
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

        private void ChangeTheme(bool isDark)
        {
            AppSettings.IsDarkTheme = isDark;

            foreach (Form form in Application.OpenForms)
            {
                if (form is standart f)
                    f.ApplyTheme();
            }
        }

    }
}
