using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smarrtCalculator
{
    public partial class hiddenForm : Form
    {
        private standart standart;
        public hiddenForm(List<string> history, standart standart)
        {
            InitializeComponent();
            ApplyTheme();

            listBox1.DataSource = null;
            listBox1.DataSource = history;
            this.standart = standart;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void hiddenForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            standart.ClearHistory();
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
