using YiSoFramework.UI;
using YiSoFramework;

namespace YiSoFramwork.Test
{
    public partial class ContactPage : YiSoPage
    {
        static IYiSoPage _pageInstant;

        public static IYiSoPage Instant => _pageInstant ??
                 (_pageInstant = new ContactPage());

        private ContactPage()
        {
            InitializeComponent();
        }
    }
}
