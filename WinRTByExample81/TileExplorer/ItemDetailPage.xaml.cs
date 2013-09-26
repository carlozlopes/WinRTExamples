﻿namespace TileExplorer
{
    using Common;
    using System;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;
    using DataModel;

    /// <summary>
    /// A page that displays details for a single item within a group.
    /// </summary>
    public sealed partial class ItemDetailPage
    {
        private readonly NavigationHelper navigationHelper;
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public ItemDetailPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            var selectedItem = (TileItem)this.flipView.SelectedItem;
            if (selectedItem != null)
            {
                e.PageState["SelectedItem"] = selectedItem.Id;
            }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            var navigationParameter = e.PageState != null && e.PageState.ContainsKey("SelectedItem")
                ? e.PageState["SelectedItem"]
                : e.NavigationParameter;
            var item = App.CurrentDataSource.GetTile((string) navigationParameter);
            var itemGroup = App.CurrentDataSource.GetGroupForItem(item.Id);
            this.DefaultViewModel["Group"] = itemGroup;
            this.DefaultViewModel["Items"] = itemGroup.Items;
            this.flipView.SelectedItem = item;
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}