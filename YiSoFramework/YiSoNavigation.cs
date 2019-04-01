namespace YiSoFramework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class YiSoNavigation : IYiSoNavigation
    {
        uint _currentPossition;
        IYiSoPage _currentPage;
        readonly List<IYiSoPage> _pages;
        readonly Stack<IYiSoPage> _navigationStack;

        #region Public Props

        public event EventHandler<EventArgs> ArrivedToLastPage;
        public event EventHandler<EventArgs> ArrivedToFirstPage;
        public event EventHandler<NavigationMovedEventArgs> MovedToLast;
        public event EventHandler<NavigationMovedEventArgs> Movedforward;
        public event EventHandler<NavigationMovedEventArgs> MovedToFirst;
        public event EventHandler<NavigationMovedEventArgs> Movedbackward;
        public event EventHandler<CurrentPageEventArgs> CurrentPageChanged;
        public event EventHandler<CurrentPageEventArgs> CurrentPossitionChanged;
        public event EventHandler<PagesListChangedEventArgs> PageListChanged;

        public IYiSoPage CurrentPage
        {
            get => _currentPage;
            set
            {
                if (value is null)
                    return;

                if (IsPageExist(value) is null)
                    throw new PageNotExistException("the page is not exist");

                _currentPage = value;
                _currentPossition = (uint)_pages.IndexOf(_currentPage);
                OnPossitionAndCurrentPageChanged(ChangeType.FromProperty);
            }
        }
        public IYiSoPage PreviousPage => GetPrevPage();
        public IYiSoPage NextPage => GetNextPage();
        public IEnumerable<IYiSoPage> NavigationStack => _navigationStack;
        public IEnumerable<IYiSoPage> Pages => _pages;
        public int PageCount => _pages.Count;
        public bool IsFirstPage => _currentPossition <= 0;
        public bool IsLastPage => _currentPossition >= 0;
        public uint CurrentPossition
        {
            get => _currentPossition;
            set
            {
                if (GetPageAt(value) is null)
                    throw new PageNotExistException("there is no page at the given position");

                _currentPossition = value;
                _currentPage = _pages[(int)_currentPossition];
                OnPossitionAndCurrentPageChanged(ChangeType.FromProperty);
            }
        }

        #endregion

        #region Constructors

        public YiSoNavigation()
        {
            _pages = new List<IYiSoPage>();
            _navigationStack = new Stack<IYiSoPage>();
        }

        #endregion

        #region Private Methods

        private void OnPossitionAndCurrentPageChanged(ChangeType type)
        {
            OnCurrentPossitionChanged(type);
            OnCurrentPageChanged(type);
        }

        private IYiSoPage GetPrevPage(bool removeFromStack = false)
        {
            if (_navigationStack.Count > 0)
                return GetPageFromNavigationStack(removeFromStack);

            if (_currentPossition <= 0)
                return null;

            return _pages[(int)_currentPossition - 1];
        }

        private IYiSoPage GetNextPage()
        {
            if (_currentPossition >= PageCount - 1)
                return null;

            return _pages[(int)_currentPossition + 1];
        }

        private void PushToNavigationStack(IYiSoPage page)
        {
            _navigationStack.Push(page);
        }

        private IYiSoPage GetPageFromNavigationStack(bool removeFromStack = false)
        {
            if (removeFromStack)
                return _navigationStack.Pop();
            else
                return _navigationStack.Peek();
        }

        #endregion

        #region Public Methods

        #region page manipulation

        /// <summary>
        /// check if the page is exit in the navigator, compare the names
        /// </summary>
        /// <param name="page">the page to look for</param>
        /// <returns>return the page if exist, null if not</returns>
        public IYiSoPage IsPageExist(IYiSoPage page)
        {
            return _pages.FirstOrDefault(p => p.PageName == page.PageName);
        }

        /// <summary>
        /// get the page at the given index
        /// </summary>
        /// <param name="index">the page index</param>
        /// <returns>return the page if exist, if not null will be returned</returns>
        public IYiSoPage GetPageAt(uint index)
        {
            return _pages.FirstOrDefault(p => p.PageIndex == index);
        }

        /// <summary>
        /// add the page to the index that is provided in PageIndex
        /// </summary>
        /// <param name="page">page to add</param>
        public void AddPage(IYiSoPage page)
        {
            var pageToFind = IsPageExist(page);

            if (!(pageToFind is null))
                throw new PageAlreadyExistException("Page with the same name Already exist");

            pageToFind = GetPageAt(page.PageIndex);

            if (!(pageToFind is null))
                throw new PageAlreadyExistAtGivenIndexException($"a page at the same index Already exist, index : '{page.PageIndex}', the page that have the index is : '{pageToFind.PageName}'");

            _pages.Insert((int)page.PageIndex, page);
            OnPageListChanged(ListChangedType.Added, null, page);
        }

        /// <summary>
        /// add the page to the index that is provided in PageIndex
        /// </summary>
        /// <param name="page">page to add</param>
        public void AddRange(IEnumerable<IYiSoPage> pages)
        {
            foreach (var page in pages)
            {
                AddPage(page);
            }
        }

        /// <summary>
        /// remove the page
        /// </summary>
        public void RemovePage(IYiSoPage page)
        {
            if (IsPageExist(page) is null)
                throw new PageNotExistException("the given page is not exist");

            _pages.Remove(page);
            OnPageListChanged(ListChangedType.Removed, page, null);
        }

        /// <summary>
        /// remove the page at the specified index
        /// </summary>
        /// <param name="index">the page index</param>
        public void RemovePageAt(uint index)
        {
            var page = GetPageAt(index) ??
                throw new PageNotExistException("there is no page at the Given index");

            _pages.Remove(page);
            OnPageListChanged(ListChangedType.Removed, page, null);
        }

        /// <summary>
        /// remove all pages
        /// </summary>
        public void Clear()
        {
            _pages.Clear();
            OnPageListChanged(ListChangedType.Cleared, null, null);
        }

        /// <summary>
        /// Update the old page with the new page
        /// </summary>
        /// <param name="oldPage">the old page</param>
        /// <param name="newPage">new page</param>
        public void UpdatePage(IYiSoPage oldPage, IYiSoPage newPage)
        {
            var oldP = IsPageExist(oldPage);
            var newP = IsPageExist(newPage);

            if (oldP is null)
                throw new PageNotExistException("the given Old Page not exist");

            if (!(newP is null))
                throw new PageAlreadyExistException("the given New Page is already exist");

            newPage.PageIndex = oldPage.PageIndex;
            _pages[(int)oldPage.PageIndex] = newPage;
            OnPageListChanged(ListChangedType.Updated, oldPage, newPage);
        }

        /// <summary>
        /// Update the old page with the new page
        /// </summary>
        /// <param name="oldPageIndex">the old page index</param>
        /// <param name="newPage">new page</param>
        public void UpdatePage(uint oldPageIndex, IYiSoPage newPage)
        {
            var oldP = GetPageAt(oldPageIndex);
            var newP = IsPageExist(newPage);

            if (oldP is null)
                throw new PageNotExistException("the given index of the Old Page not exist");

            if (!(newP is null))
                throw new PageAlreadyExistException("the given New Page is already exist");

            newPage.PageIndex = oldPageIndex;
            _pages[(int)oldPageIndex] = newPage;
            OnPageListChanged(ListChangedType.Updated, oldP, newPage);
        }

        /// <summary>
        /// swap the two pages
        /// </summary>
        /// <param name="page1Index">the first page</param>
        /// <param name="page2Index">the second page</param>
        public void SwapPages(uint page1Index, uint page2Index)
        {
            var page1 = GetPageAt(page1Index);
            var page2 = GetPageAt(page2Index);

            if (page1 is null)
                throw new PageNotExistException("page1 is not exist");

            if (page2 is null)
                throw new PageNotExistException("page2 is not exist");

            // swap the pages first
            var temp = _pages[(int)page1Index];
            _pages[(int)page1Index] = _pages[(int)page2Index];
            _pages[(int)page2Index] = temp;

            //the we swap the indexes
            var tempIndex = _pages[(int)page1Index].PageIndex;
            _pages[(int)page1Index].PageIndex = _pages[(int)page2Index].PageIndex;
            _pages[(int)page2Index].PageIndex = tempIndex;
        }

        /// <summary>
        /// swap the two pages
        /// </summary>
        /// <param name="page1">the first page</param>
        /// <param name="page2">the second page</param>
        public void SwapPages(IYiSoPage page1, IYiSoPage page2)
        {
            if (page1 is null || page2 is null)
                throw new ArgumentNullException($"the page{(page1 is null ? 2 : 1)} is null");

            SwapPages(page1.PageIndex, page2.PageIndex);
        }

        #endregion

        #region Navigation Methods

        /// <summary>
        /// Move next
        /// </summary>
        public void MoveNext()
        {
            var page = GetNextPage();
            if (page is null)
                return;

            var lastIndex = PageCount - 1;
            if (_currentPossition++ >= lastIndex)
                _currentPossition = (uint)lastIndex;

            var oldCurrentPageIndex = _currentPage.PageIndex;
            PushToNavigationStack(_currentPage);
            _currentPage = page;
            OnMovedforward(oldCurrentPageIndex);
        }

        /// <summary>
        /// Move previous
        /// </summary>
        public void MovePrevious()
        {
            var page = GetPrevPage(true);
            if (page is null)
                return;

            if (_currentPossition-- <= 0)
                _currentPossition = 0;

            var oldCurrentPageIndex = _currentPage.PageIndex;
            _currentPage = page;
            OnMovedbackward(oldCurrentPageIndex);

            //if (_currentPossition == 0)
            //    return;

            //_currentPossition--;

            //if (_currentPossition <= 0)
            //    _currentPossition = 0;

            //var oldCurrentPageIndex = _currentPage.PageIndex;
            //_currentPage = _pages[(int)_currentPossition];
            //OnMovedbackward(oldCurrentPageIndex);
        }

        /// <summary>
        /// Move First
        /// </summary>
        public void MoveFirst()
        {
            var oldCurrentPageIndex = _currentPage.PageIndex;
            _currentPossition = 0;
            _currentPage = _pages[0];
            _navigationStack.Clear();
            OnMovedToFirst(oldCurrentPageIndex);
        }

        /// <summary>
        /// Move Last
        /// </summary>
        public void MoveLast()
        {
            var oldCurrentPageIndex = _currentPage.PageIndex;
            _currentPossition = (uint)PageCount - 1;
            PushToNavigationStack(_currentPage);
            _currentPage = _pages[(int)_currentPossition];
            OnMovedToLast(oldCurrentPageIndex);
        }

        /// <summary>
        /// Move previous
        /// </summary>
        public void MoveTo(uint index)
        {
            if (GetPageAt(index) is null)
                throw new PageNotExistException("there is no page at the given index");

            var oldCurrentPageIndex = _currentPage.PageIndex;
            _currentPossition = index;
            _currentPage = _pages[(int)_currentPossition];

            if (oldCurrentPageIndex > index)
                OnMovedbackward(oldCurrentPageIndex);
            else
                OnMovedforward(oldCurrentPageIndex);
        }

        /// <summary>
        /// Move previous
        /// </summary>
        public void MoveTo(string pageName)
        {
            var page = _pages.FirstOrDefault(p => p.PageName == pageName) ??
                throw new PageNotExistException("there is no page with the given name");

            var oldCurrentPageIndex = _currentPage.PageIndex;
            _currentPage = page;
            _currentPossition = page.PageIndex;

            if (oldCurrentPageIndex > page.PageIndex)
                OnMovedbackward(oldCurrentPageIndex);
            else
                OnMovedforward(oldCurrentPageIndex);
        }

        #endregion

        private void OnCurrentPageChanged(ChangeType type)
        {
            CurrentPageChanged?.Invoke(this, new CurrentPageEventArgs(type));
        }

        private void OnCurrentPossitionChanged(ChangeType type)
        {
            CurrentPossitionChanged?.Invoke(this, new CurrentPageEventArgs(type));
        }

        private void OnPageListChanged(ListChangedType type, IYiSoPage oldPage, IYiSoPage newPage)
        {
            PageListChanged?.Invoke(this, new PagesListChangedEventArgs(type, oldPage, newPage));
        }

        private void OnMovedforward(uint oldCurrentPageIndex)
        {
            Movedforward?.Invoke(this, new NavigationMovedEventArgs(MovingType.Forward, _currentPage, _pages[(int)oldCurrentPageIndex]));

            if (_currentPossition >= PageCount - 1)
                ArrivedToLastPage?.Invoke(this, new EventArgs());

            OnPossitionAndCurrentPageChanged(ChangeType.FromNavigationMethods);
        }

        private void OnMovedbackward(uint oldCurrentPageIndex)
        {
            Movedbackward?.Invoke(this, new NavigationMovedEventArgs(MovingType.Backward, _currentPage, _pages[(int)oldCurrentPageIndex]));

            if (_currentPossition <= 0)
                ArrivedToFirstPage?.Invoke(this, new EventArgs());

            OnPossitionAndCurrentPageChanged(ChangeType.FromNavigationMethods);
        }

        private void OnMovedToFirst(uint oldCurrentPageIndex)
        {
            MovedToFirst?.Invoke(this, new NavigationMovedEventArgs(MovingType.Backward, _currentPage, _pages[(int)oldCurrentPageIndex]));
            ArrivedToFirstPage?.Invoke(this, new EventArgs());
            OnPossitionAndCurrentPageChanged(ChangeType.FromNavigationMethods);
        }

        private void OnMovedToLast(uint oldCurrentPageIndex)
        {
            MovedToLast?.Invoke(this, new NavigationMovedEventArgs(MovingType.Forward, _currentPage, _pages[(int)oldCurrentPageIndex]));
            ArrivedToLastPage?.Invoke(this, new EventArgs());
            OnPossitionAndCurrentPageChanged(ChangeType.FromNavigationMethods);
        }

        #endregion
    }
}
