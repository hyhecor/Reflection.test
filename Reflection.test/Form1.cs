using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Reflection.ReflectionForm;

namespace Reflection.test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dllName = "subForm.test.dll";
            string nameSpace = "subForm.test";
            string className = "subForm";
            
            Form frm = (new ReflectionForm()).GetDllFormInstance(
                dllName
                , nameSpace
                , className);

            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nameSpace = "Reflection.test";
            string className = "Form2";

            Form frm = (new ReflectionForm(Options.NoDuplicate)).GetFormInstance(
                nameSpace, className);

            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string nameSpace = "Reflection.test.Form3";
            string className = "Form3";

            Form frm = (new ReflectionForm(Options.NoDuplicate | Options.Activate)).GetFormInstance(
                nameSpace
                , className);

            frm.Show();
        }
    }
}
