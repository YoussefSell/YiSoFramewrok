namespace YiSoFramework
{
    using System;
    using System.Collections.Generic;

    public interface IYiSoNavigation
    {
        IYiSoPage CurrentPage { get; set; }
        uint CurrentPossition { get; set; }
        int PageCount { get; }
        IEnumerable<IYiSoPage> Pages { get; }
        IEnumerable<IYiSoPage> NavigationStack { get; }
        IYiSoPage PreviousPage { get; }
        IYiSoPage NextPage { get; }
        bool IsFirstPage { get; }
        bool IsLastPage { get; }

        event EventHandler<EventArgs> ArrivedToLastPage;
        event EventHandler<EventArgs> ArrivedToFirstPage;
        event EventHandler<NavigationMovedEventArgs> MovedToLast;
        event EventHandler<NavigationMovedEventArgs> MovedToFirst;
        event EventHandler<NavigationMovedEventArgs> Movedforward;
        event EventHandler<NavigationMovedEventArgs> Movedbackward;
        event EventHandler<CurrentPageEventArgs> CurrentPageChanged;
        event EventHandler<PagesListChangedEventArgs> PageListChanged;
        event EventHandler<CurrentPageEventArgs> CurrentPossitionChanged;

        IYiSoPage GetPageAt(uint index);
        IYiSoPage IsPageExist(IYiSoPage page);
        void AddPage(IYiSoPage page);
        void AddRange(IEnumerable<IYiSoPage> pages);
        void Clear();
        void MoveFirst();
        void MoveLast();
        void MoveNext();
        void MovePrevious();
        void MoveTo(string pageName);
        void MoveTo(uint index);
        void RemovePage(IYiSoPage page);
        void RemovePageAt(uint index);
        void SwapPages(IYiSoPage page1, IYiSoPage page2);
        void SwapPages(uint page1Index, uint page2Index);
        void UpdatePage(IYiSoPage oldPage, IYiSoPage newPage);
        void UpdatePage(uint oldPageIndex, IYiSoPage newPage);
    }
}