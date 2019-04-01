using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace YiSoFramework.UI
{
    public partial class YiSoPagesContainer : UserControl, IYiSoPagesContainer
    {
        Point DefaultPoint;
        Point BackwordPoint;
        Point ForwardPoint;

        private bool _moveForward;
        private IYiSoNavigation _navigator;
        private YiSoPage _currentPage;

        /// <summary>
        /// the Site reference
        /// </summary>
        public override ISite Site
        {
            get => base.Site;
            set
            {
                base.Site = value;
                if (value is null)
                    return;

                if (value.GetService(typeof(IDesignerHost)) is IDesignerHost service)
                {
                    if (service.RootComponent is ContainerControl rootComponent)
                        ContainerControl = rootComponent;
                }
            }
        }

        /// <summary>
        /// represent an instant of the form that holds the YiSoDragControl instant
        /// </summary>
        public ContainerControl ContainerControl { get; private set; }

        public YiSoPagesContainer()
        {
            InitializeComponent();
            DefaultPoint = new Point(0, 0);
            ForwardPoint = new Point(Width/2, 0);
            BackwordPoint = new Point(-Width/2, 0);
        }

        public IYiSoNavigation Navigator
        {
            get => _navigator;
            set
            {
                _navigator = value;
                SetNavigator();
            }
        }
        public IYiSoPage CurrentPage
        {
            get => _currentPage;
            private set
            {
                RemovePage();
                _currentPage = value as YiSoPage;
                _currentPage.Location = DefaultPoint;
                _currentPage.Size = new Size(Width, Height);
                Controls.Add(_currentPage as YiSoPage);
                yiSoDragger1.TargetControl = _currentPage as YiSoPage;
            }
        }
        public IYiSoPage PreviousPage { get => Navigator.PreviousPage; }
        public IYiSoPage NextPage { get => Navigator.NextPage; }
        public bool IsFirstPage {  get => Navigator?.IsFirstPage ?? false; }
        public bool IsLastPage { get => Navigator?.IsLastPage ?? false; }
        public bool HoveHorizontal { get; set; } = true;

        public void RemovePage()
        {
            Controls.Clear();
        }

        public void HidePage()
        {
            CurrentPage.Hide();
        }

        public void ShowPage()
        {
            if (CurrentPage is null)
                throw new ArgumentNullException("the Current page is null");

            CurrentPage.Show();
        }

        private void SetNavigator()
        {
            if (!(_navigator is null))
            {
                Navigator.Movedforward += Navigator_Movedforward;
                Navigator.Movedbackward += Navigator_Movedbackward;
                Navigator.MovedToFirst += Navigator_MovedToFirst;
                Navigator.MovedToLast += Navigator_MovedToLast;
                Navigator.CurrentPageChanged += Navigator_CurrentPageChanged;
            }
        }

        private void Navigate()
        {
            timer1.Start();
        }

        private void Navigator_CurrentPageChanged(object sender, CurrentPageEventArgs e)
        {
            if (e.Type == ChangeType.FromProperty)
            {
                CurrentPage = Navigator.CurrentPage;
            }
        }

        private void Navigator_MovedToLast(object sender, NavigationMovedEventArgs e)
        {
        }

        private void Navigator_MovedToFirst(object sender, NavigationMovedEventArgs e)
        {
        }

        private void Navigator_Movedforward(object sender, NavigationMovedEventArgs e)
        {
            _moveForward = true;
            SetCurrentPage(e.CurrentPage, ForwardPoint);
            Navigate();
        }

        private void Navigator_Movedbackward(object sender, NavigationMovedEventArgs e)
        {
            _moveForward = false;
            SetCurrentPage(e.CurrentPage, BackwordPoint);
            Navigate();
        }

        private void SetCurrentPage(IYiSoPage value, Point point)
        {
            RemovePage();
            _currentPage = value as YiSoPage;
            _currentPage.Location = point;
            _currentPage.Size = new Size(Width, Height);
            Controls.Add(_currentPage as YiSoPage);
            yiSoDragger1.TargetControl = _currentPage as YiSoPage;
        }

        private void YiSoPagesContainer_Load(object sender, EventArgs e)
        {

        }

        private void YiSoDragger_ControlReleased(object sender, EventArgs e)
        {
            if (_currentPage.Location.X <= Width / 2)
            {
                _currentPage.Location = new System.Drawing.Point(0, 0);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (_moveForward)
            {
                _currentPage.Left -= 5;
                if (_currentPage.Location.X <= 0)
                {
                    _currentPage.Location = DefaultPoint;
                    timer1.Stop();
                }
            }
            else
            {
                _currentPage.Left += 5;
                if (_currentPage.Location.X >= 0)
                {
                    _currentPage.Location = DefaultPoint;
                    timer1.Stop();
                }
            }
        }
    }


}
