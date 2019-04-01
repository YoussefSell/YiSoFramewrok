using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace YiSoFramework.UI
{
    /// <summary>
    /// class used to move A form with dragging
    /// </summary>
    public partial class YiSoDragger : Component
    {
        private Control _targetControl;
        private Control _formContainer;

        #region public props

        /// <summary>
        /// represent an instant of the form that holds the YiSoDragControl instant
        /// </summary>
        public ContainerControl FormToDrag { get; private set; }

        /// <summary>
        /// specify the target control for the drag effect
        /// </summary>
        public Control TargetControl
        {
            get => _targetControl;
            set
            {
                RemoveControl();
                _targetControl = value;
                _formContainer = FormToDrag;
                InitializeControl();
            }
        }

        /// <summary>
        /// for making the target control fixed 
        /// </summary>
        public bool Fixed { get; set; } = false;

        /// <summary>
        /// for controlling the vertical movement
        /// </summary>
        public bool VerticalMoving { get; set; } = true;

        /// <summary>
        /// for controlling the Horizontal movement
        /// </summary>
        public bool HorizontalMoving { get; set; } = true;

        /// <summary>
        /// check if the target control is Released
        /// </summary>
        public bool IsReleased { get; private set; } = true;

        /// <summary>
        /// get the X position of the mouse
        /// </summary>
        public int MousePositionX { get; private set; }

        /// <summary>
        /// get the Y position of the mouse
        /// </summary>
        public int MousePositionY { get; private set; }

        /// <summary>
        /// use the control as dragger to move the form
        /// </summary>
        public bool UseAsFormDrager { get; set; } = true;

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
                        FormToDrag = rootComponent;
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// the default constructor for initializing a YiSo_Elipse object
        /// </summary>
        public YiSoDragger()
        {
            InitializeComponent();
        }

        /// <summary>
        /// the constructor for initializing a YiSo_Elipse object with a container
        /// </summary>
        /// <param name="container">add the container to the container control</param>
        public YiSoDragger(IContainer container) : this()
        {
            container.Add(this);
        }

        #endregion

        #region Events
        /// <summary>
        /// event raised when the target control is moved
        /// </summary>
        public event EventHandler ControlMoved;

        /// <summary>
        /// event raised when the target control is Released
        /// </summary>
        public event EventHandler ControlReleased;

        #endregion

        #region private methods

        //method for Moving the target Control
        private void MouseMoveEvent(object sender, MouseEventArgs e)
        {
            MoveControl(VerticalMoving, HorizontalMoving);
        }

        //method for Releasing the target Control
        private void MouseUpEvent(object sender, MouseEventArgs e)
        {
            IsReleased = true;
            ControlReleased?.Invoke(this, new EventArgs());
        }

        //method for grabbing the target Control
        private void MouseDownEvent(object sender, MouseEventArgs e)
        {
            if (!Fixed)
            {
                GrabControl();
                ControlMoved?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// starting the dragging effect by initializing the specific property for the control
        /// </summary>
        /// <param name="targetControl">the target control for the drag effect </param>
        private void GrabControl()
        {
            var controlInstant = GetTargetControl();

            IsReleased = false;
            MousePositionX = Control.MousePosition.X - controlInstant.Left;
            MousePositionY = Control.MousePosition.Y - controlInstant.Top;
        }

        /// <summary>
        /// specify that the target control is Moving
        /// </summary>
        /// <param name="horizontal">specify the horizontal movement</param>
        /// <param name="vertical">specify the vertical movement</param>
        private void MoveControl(bool horizontal = true, bool vertical = true)
        {
            if (!IsReleased)
            {
                Point mousePosition = Control.MousePosition;
                int x = mousePosition.X;
                int y = mousePosition.Y;

                var controlInstant = GetTargetControl();

                if (vertical)
                    controlInstant.Top = y - MousePositionY;
                if (horizontal)
                    controlInstant.Left = x - MousePositionX;
            }
        }

        /// <summary>
        /// get the specific target control
        /// </summary>
        /// <returns>control</returns>
        private Control GetTargetControl()
        {
            var controlInstant = UseAsFormDrager ? _formContainer : _targetControl;

            if (controlInstant is null)
                throw new ArgumentNullException(nameof(TargetControl), "the target control is null");

            return controlInstant;
        }

        /// <summary>
        /// Initialize the control
        /// </summary>
        private void InitializeControl()
        {
            if (_targetControl != null)
            {
                _targetControl.MouseDown += new MouseEventHandler(MouseDownEvent);
                _targetControl.MouseMove += new MouseEventHandler(MouseMoveEvent);
                _targetControl.MouseUp += new MouseEventHandler(MouseUpEvent);
            }
        }

        private void RemoveControl()
        {
            if (!(_targetControl is null))
            {
                _targetControl.MouseDown -= MouseDownEvent;
                _targetControl.MouseMove -= MouseMoveEvent;
                _targetControl.MouseUp -= MouseUpEvent;
            }
        }

        #endregion
    }
}
