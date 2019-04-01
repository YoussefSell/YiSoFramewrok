using System;
using YiSoFramework;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace YiSoFramework.UI
{
    public partial class YiSoEllipser : Component
    {
        #region private properties

        private int _radius = 5;
        private Control _targetControl;

        #endregion

        #region Public Props

        /// <summary>
        /// represent an instant of the form that holds the YiSoDragControl instant
        /// </summary>
        public ContainerControl ContainerControl { get; private set; }

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
        /// specify the target control for the Ellipser
        /// </summary>
        public Control TargetControl
        {
            get => _targetControl;
            set
            {
                RemoveControl();
                _targetControl = value;
                InitializeControl();
            }
        }

        /// <summary>
        /// the Radius to Apply to the control
        /// </summary>
        public int Radius
        {
            get => _radius;
            set
            {
                _radius = value < 0 ? 0 : value;
                ApplyRoundedEdges();
            }
        }

        #endregion

        #region Constructors

        public YiSoEllipser()
        {
            InitializeComponent();
        }

        public YiSoEllipser(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// event raised when the target control get the Ellipse Effected Applied
        /// </summary>
        public event EventHandler EllipseEffectedApplied;

        /// <summary>
        /// event raised when the target control get the Ellipse Effected UnApplied
        /// </summary>
        public event EventHandler EllipseEffectedUnApplied;

        /// <summary>
        /// invoke the EllipseEffectedApplied
        /// </summary>
        public void OnEllipseEffectedApplied()
        {
            EllipseEffectedApplied?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// invoke the EllipseEffectedUnApplied
        /// </summary>
        public void OnEllipseEffectedUnApplied()
        {
            EllipseEffectedUnApplied?.Invoke(this, new EventArgs());
        }

        #endregion

        #region Private Methods

        private void InitializeControl()
        {
            if (!(TargetControl is null))
            {
                TargetControl.Resize += TargetControl_Resize;
                ApplyRoundedEdges();
            }
        }

        private void RemoveControl()
        {
            if (!(TargetControl is null))
            {
                UnApplyRoundedEdges();
                TargetControl.Resize -= TargetControl_Resize;
            }
        }

        private void TargetControl_Resize(object sender, EventArgs e)
        {
            ApplyRoundedEdges();
        }

        #endregion

        #region Public Methods

        public void ApplyRoundedEdges()
        {
            if (!(TargetControl is null))
            {
                ApplyRoundedEdges(TargetControl, Radius);
                OnEllipseEffectedApplied();
            }
        }

        public void UnApplyRoundedEdges()
        {
            if (!(TargetControl is null))
            {
                UnApplyRoundedEdges(TargetControl);
                OnEllipseEffectedUnApplied();
            }
        }

        public static void ApplyRoundedEdges(Control control, int radius)
        {
            YiSoElipse.ApplyRoundedEdges(control, radius);
        }

        public static void UnApplyRoundedEdges(Control control)
        {
            YiSoElipse.UnApplyRoundedEdges(control);
        }

        #endregion
    }
}
