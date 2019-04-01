using YiSoFramework.UI;
using YiSoFramework;

namespace YiSoFramwork.Test
{
    public partial class DashPage : YiSoPage
    {
        static IYiSoPage _pageInstant;

        public static IYiSoPage Instant => _pageInstant ??
                 (_pageInstant = new DashPage());

        private DashPage()
        {
            InitializeComponent();
        }

        private void DashPage_Load(object sender, System.EventArgs e)
        {

        }
    }
}
