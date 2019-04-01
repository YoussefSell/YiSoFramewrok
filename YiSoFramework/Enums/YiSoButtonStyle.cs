namespace YiSoFramework
{
    public enum ChangeType
    {
        FromProperty,
        FromNavigationMethods
    }

    public enum MovingType
    {
        Forward,
        Backward,
        ToFirst,
        ToLast,
    }

    public enum ListChangedType
    {
        Added,
        Removed,
        Updated,
        Cleared
    }

    public interface IYiSoPagesContainer
    {
        IYiSoPage PreviousPage { get; }
        IYiSoPage CurrentPage { get; }
        IYiSoPage NextPage { get; }
        IYiSoNavigation Navigator { get; set; }

        bool IsFirstPage { get; }
        bool IsLastPage { get; }

        void ShowPage();
        void HidePage();
        void RemovePage();
    }

    public interface IYiSoPage
    {
        int Left { get; set; }
        System.Drawing.Size Size { get; set; }
        System.Drawing.Point Location { get; set; }
        System.Windows.Forms.DockStyle Dock { get; set; }
        string PageName { get; set; }
        uint PageIndex { get; set; }
        void Show();
        void Hide();
    }

    public enum YiSoButtonStyle
    {
        DefaultButton,
        IconButton,
        OutlinedButton,
        TextButton
    }

    public enum YiSoFontSize
    {
        Size12 = 12,
        Size18 = 18,
        Size24 = 24,
        Size36 = 36,
        Size48 = 48,
        Size60 = 60,
        Size72 = 72
    }

    public enum YiSoFontWeight
    {
        Bold = 0,
        Light = 1,
        Medium = 2,
        Regular = 3,
        Thin = 4,
        Black = 5,
        Italic = 6
    }

    public enum YiSoiConsList
    {
        none,
        CheckIcon,
        ErrorIcon,
        warning
    }
}
