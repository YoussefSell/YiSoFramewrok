using System;
using YiSoFramework.UI;
using YiSoFramework;

namespace YiSoFramwork.Test
{
    public partial class AboutPage : YiSoPage
    {
        static IYiSoPage _pageInstant;

        public static IYiSoPage Instant => _pageInstant ??
                 (_pageInstant = new AboutPage());

        private AboutPage()
        {
            InitializeComponent();
        }

        private void AboutPage_Load(object sender, EventArgs e)
        {

        }
    }
}
