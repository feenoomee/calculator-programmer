using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace smarrtCalculator
{
    public partial class parametrs : Form
    {
        public parametrs()
        {
            InitializeComponent();
            ApplyTheme();
            //Helper.SetFontSize(this, AppSettings.FontSize);
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

        private void button3_Click(object sender, EventArgs e)
        {
            AppSettings.IsDarkTheme = false;
            ApplyThemeToAllForms();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AppSettings.IsDarkTheme = true;
            ApplyThemeToAllForms();
        }

        // Метод для применения темы ко всем открытым формам
        private void ApplyThemeToAllForms()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is standart s) s.ApplyTheme();
                else if (form is programist p) p.ApplyTheme();
                else if (form is parametrs par) par.ApplyTheme();
                else if (form is hiddenForm x) x.ApplyTheme();
                else if (form is hiddenForm2 b) b.ApplyTheme();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            standart standartForm = new standart();
            standartForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            programist programmistForm = new programist();
            programmistForm.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int size))
            {
                AppSettings.FontSize = size;
                ApplyFontSizeToAllForms();
            }
        }
        private void ApplyFontSizeToAllForms()
        {
            foreach (Form form in Application.OpenForms)
            {
                SetFontSize(form, AppSettings.FontSize);
            }
        }

        private void SetFontSize(Control parent, int size)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Font = new Font(ctrl.Font.FontFamily, size);
                if (ctrl.HasChildren)
                    SetFontSize(ctrl, size);
            }
        }
    }
}
