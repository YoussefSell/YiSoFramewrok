namespace YiSoFramework
{
    using System.Windows.Forms;

    public interface IYiSoComponet
    {
        ContainerControl ContainerControl { get; }
        Control TargetControl { get; set; }

        void InitializeControl();
        void RemoveControl();

        void Start();
        void Stop();
    }
}