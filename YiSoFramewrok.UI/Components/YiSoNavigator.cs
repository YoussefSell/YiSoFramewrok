using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace YiSoFramework.UI
{
    public partial class YiSoNavigator : Component, INotifyPropertyChanged
    {
        uint _currentPossition;
        YiSoPage _currentPage;
        List<YiSoPage> _pages;

        #region Public Props

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

        public event PropertyChangedEventHandler PropertyChanged;

        public YiSoPagesContainer PagesContainer { get; set; }

        public IEnumerable<YiSoPage> Pages { get => _pages; }

        public int PageCount { get => _pages.Count; }

        [Browsable(false)]
        public uint CurrentPossition
        {
            get => _currentPossition;
            set
            {
                if (GetPageAt(value) is null && value != 0)
                    throw new PageNotExistException("there is no page at the given position");

                _currentPossition = value;
                _currentPage = _pages[(int)_currentPossition];
                PossitionChanged();
            }
        }


        [Browsable(false)]
        public YiSoPage CurrentPage
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
                CurrentPageChanged();
            }
        }

        #endregion

        #region Constructors

        public YiSoNavigator()
        {
            InitializeComponent();
            _pages = new List<YiSoPage>();
        }

        public YiSoNavigator(IContainer container) 
            : this()
        {
            container.Add(this);
        }

        #endregion

        #region Private Methods

        private void PossitionChanged()
        {
            OnPropertyChanged(nameof(CurrentPage));
            OnPropertyChanged(nameof(CurrentPossition));
        }

        private void CurrentPageChanged()
        {
            OnPropertyChanged(nameof(CurrentPage));
            OnPropertyChanged(nameof(CurrentPossition));
        }

        #endregion

        #region Public Methods

        #region page manipulation

        /// <summary>
        /// check if the page is exit in the navigator, compare the names
        /// </summary>
        /// <param name="page">the page to look for</param>
        /// <returns>return the page if exist, null if not</returns>
        public YiSoPage IsPageExist(YiSoPage page)
        {
            return _pages.FirstOrDefault(p => p.PageName == page.PageName);
        }

        /// <summary>
        /// get the page at the given index
        /// </summary>
        /// <param name="index">the page index</param>
        /// <returns>return the page if exist, if not null will be returned</returns>
        public YiSoPage GetPageAt(uint index)
        {
            return _pages.FirstOrDefault(p => p.PageIndex == index);
        }

        /// <summary>
        /// add the page to the index that is provided in PageIndex
        /// </summary>
        /// <param name="page">page to add</param>
        public void AddPage(YiSoPage page)
        {
            var pageToFind = IsPageExist(page);

            if (!(pageToFind is null))
                throw new PageAlreadyExistException("Page with the same name Already exist");

            pageToFind = GetPageAt(page.PageIndex);

            if (!(pageToFind is null))
                throw new PageAlreadyExistAtGivenIndexException($"a page at the same index Already exist, index : '{page.PageIndex}', the page that have the index is : '{pageToFind.PageName}'");

            _pages.Insert((int)page.PageIndex, page);
        }

        /// <summary>
        /// add the page to the index that is provided in PageIndex
        /// </summary>
        /// <param name="page">page to add</param>
        public void AddRange(IEnumerable<YiSoPage> pages)
        {
            foreach (var page in pages)
            {
                AddPage(page);
            }
        }

        /// <summary>
        /// remove the page
        /// </summary>
        public void RemovePage(YiSoPage page)
        {
            _pages.Remove(page);
        }

        /// <summary>
        /// remove the page at the specified index
        /// </summary>
        /// <param name="index">the page index</param>
        public void RemovePageAt(uint index)
        {
            _pages.RemoveAt((int)index);
        }

        /// <summary>
        /// remove all pages
        /// </summary>
        public void Clear()
        {
            _pages.Clear();
        }

        /// <summary>
        /// Update the old page with the new page
        /// </summary>
        /// <param name="oldPage">the old page</param>
        /// <param name="newPage">new page</param>
        public void UpdatePage(YiSoPage oldPage, YiSoPage newPage)
        {
            var oldP = IsPageExist(oldPage);
            var newP = IsPageExist(newPage);

            if (oldP is null)
                throw new PageNotExistException("the given Old Page not exist");

            if (!(newP is null))
                throw new PageAlreadyExistException("the given New Page is already exist");

            newPage.PageIndex = oldPage.PageIndex;
            RemovePage(oldPage);
            AddPage(newPage);
        }

        /// <summary>
        /// Update the old page with the new page
        /// </summary>
        /// <param name="oldPageIndex">the old page index</param>
        /// <param name="newPage">new page</param>
        public void UpdatePage(uint oldPageIndex, YiSoPage newPage)
        {
            var oldP = GetPageAt(oldPageIndex);
            var newP = IsPageExist(newPage);

            if (oldP is null)
                throw new PageNotExistException("the given Old Page not exist");

            if (!(newP is null))
                throw new PageAlreadyExistException("the given New Page is already exist");

            newPage.PageIndex = oldP.PageIndex;
            RemovePage(oldP);
            AddPage(newPage);
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
                throw new PageNotExistException("there is no page at the given Page1index");

            if (page2 is null)
                throw new PageNotExistException("there is no page at the given Page2index");

            var temp = _pages[(int)page1Index];
            _pages[(int)page1Index] = _pages[(int)page2Index];
            _pages[(int)page2Index] = temp;
        }

        /// <summary>
        /// swap the two pages
        /// </summary>
        /// <param name="page1">the first page</param>
        /// <param name="page2">the second page</param>
        public void SwapPages(YiSoPage page1, YiSoPage page2)
        {
            if (page1 is null || page2 is null)
                throw new ArgumentNullException($"the page{(page1 is null ? 2 : 1)} is null");

            var pageOne = IsPageExist(page1);
            var pageTwo = IsPageExist(page2);

            if (pageOne is null)
                throw new PageNotExistException("page1 not exist");

            if (pageTwo is null)
                throw new PageNotExistException("page2 not exist");

            SwapPages(pageOne.PageIndex, pageTwo.PageIndex);
        }

        #endregion

        #region Moving Methods

        /// <summary>
        /// Move next
        /// </summary>
        public void MoveNext()
        {
            var lastIndex = PageCount - 1;
            _currentPossition++;

            if (_currentPossition >= lastIndex)
                _currentPossition = (uint)lastIndex;

            _currentPage = _pages[(int)_currentPossition];
            CurrentPageChanged();
        }

        /// <summary>
        /// Move previous
        /// </summary>
        public void MovePreveiose()
        {
            _currentPossition--;
            if (_currentPossition <= 0)
                _currentPossition = 0;

            _currentPage = _pages[(int)_currentPossition];
            CurrentPageChanged();
        }

        /// <summary>
        /// Move First
        /// </summary>
        public void MoveFirst()
        {
            _currentPossition = 0;
            _currentPage = _pages[0];
            CurrentPageChanged();
        }

        /// <summary>
        /// Move Last
        /// </summary>
        public void MoveLast()
        {
            _currentPossition = (uint)PageCount - 1;
            _currentPage = _pages[(int)_currentPossition];
            CurrentPageChanged();
        }

        /// <summary>
        /// Move previous
        /// </summary>
        public void MoveTo(uint index)
        {
            if (GetPageAt(index) is null)
                throw new PageNotExistException("there is no page at the given index");

            _currentPossition = index;
            _currentPage = _pages[(int)_currentPossition];
            CurrentPageChanged();
        }

        /// <summary>
        /// Move previous
        /// </summary>
        public void MoveTo(string pageName)
        {
            var page = _pages.FirstOrDefault(p => p.PageName == pageName) ??
                throw new PageNotExistException("there is no page with the given name");

            _currentPage = page;
            _currentPossition = page.PageIndex;
            CurrentPageChanged();
        }

        #endregion

        public void OnPropertyChanged([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion
    }
}
