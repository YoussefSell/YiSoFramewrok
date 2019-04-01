using System.Windows.Forms;

namespace YiSoFramework.UI
{
    public partial class YiSoPage : UserControl, IYiSoPage
    {
        public string PageName { get; set; }
        public uint PageIndex { get; set; }

        protected YiSoPage()
        {
            InitializeComponent();
        }

        private void YiSoPage_Load(object sender, System.EventArgs e)
        {
            
        }
    }
}
