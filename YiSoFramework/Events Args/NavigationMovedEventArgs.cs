namespace YiSoFramework
{
    using System;

    public class CurrentPageEventArgs : EventArgs
    {
        public CurrentPageEventArgs(ChangeType type)
        {
            Type = type;
        }

        public ChangeType Type { get; set; }

    }

    public class NavigationMovedEventArgs : EventArgs
    {
        /// <summary>
        /// the movement type
        /// </summary>
        public MovingType MovingType { get; }

        /// <summary>
        /// the current selected page
        /// </summary>
        public IYiSoPage CurrentPage { get; }

        /// <summary>
        /// the page that we navigate from
        /// </summary>
        public IYiSoPage MovedFrom { get; }

        /// <summary>
        /// the index of the current selected page
        /// </summary>
        public uint CurrentPageIndex { get => CurrentPage.PageIndex; }

        /// <summary>
        /// the index of the page we moved from
        /// </summary>
        public uint MoveFromIndex { get => MovedFrom.PageIndex; }

        /// <summary>
        /// constructor with full parameters
        /// </summary>
        /// <param name="movingType">the movement type</param>
        /// <param name="currentPage">the current selected page</param>
        /// <param name="movedFrom">the page we moved from</param>
        public NavigationMovedEventArgs(MovingType movingType, IYiSoPage currentPage, IYiSoPage movedFrom)
        {
            MovingType = movingType;
            CurrentPage = currentPage;
            MovedFrom = movedFrom;
        }
    }
}
