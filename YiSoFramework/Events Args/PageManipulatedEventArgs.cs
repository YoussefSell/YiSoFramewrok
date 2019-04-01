namespace YiSoFramework
{
    public class PagesListChangedEventArgs : System.EventArgs
    {
        /// <summary>
        /// type of the change
        /// </summary>
        public ListChangedType Type { get; set; }

        /// <summary>
        /// the old page, null in case of add
        /// </summary>
        public IYiSoPage OldPage { get; set; }

        /// <summary>
        /// the new page, null in case of delete
        /// </summary>
        public IYiSoPage NewPage { get; set; }

        /// <summary>
        /// the index of the old page
        /// </summary>
        public uint OldPageIndex { get => OldPage.PageIndex; }

        /// <summary>
        /// the index of the new page
        /// </summary>
        public uint NewPageIndex { get => NewPage.PageIndex; }

        /// <summary>
        /// the constructor with all properties
        /// </summary>
        /// <param name="type">the change type</param>
        /// <param name="oldPage">the old page</param>
        /// <param name="newPage">the new page</param>
        public PagesListChangedEventArgs(ListChangedType type, IYiSoPage oldPage, IYiSoPage newPage)
        {
            Type = type;
            OldPage = oldPage;
            NewPage = newPage;
        }
    }
}
