using System;
using System.Windows.Forms;
using YiSoFramework;

namespace YiSoFramwork.Test
{
    public partial class Form1 : Form
    {
        DashPage DashPage;
        ContactPage ContactPage;
        YiSoNavigation Navigator;
        AboutPage AboutPage;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            DashPage = DashPage.Instant as DashPage;
            ContactPage = ContactPage.Instant as ContactPage;
            AboutPage = AboutPage.Instant as AboutPage;
            Navigator = new YiSoNavigation();
            yiSoPagesContainer1.Navigator = Navigator;

            Navigator.AddPage(DashPage);
            Navigator.AddPage(ContactPage);
            Navigator.AddPage(AboutPage);
            Navigator.CurrentPossition = 0;

        }

        private void Btn_ChangeValue_Click(object sender, EventArgs e)
        {
            var Value = Txt_Value.Text.ToInt();
            
        }

        //Trigger button
        private void Btn_Trigger_Click(object sender, EventArgs e)
        {
            Navigator.MoveNext();
        }

        //DeTrigger
        private void Btn_DeTrigger_Click(object sender, EventArgs e)
        {
            Navigator.MovePrevious();
        }
    }
}
