namespace CPW___219_eCommerceSite.Models
{
    public class ItemCatalogViewModel
    {
        public ItemCatalogViewModel(List<item> items, int lastPage, int currPage)
        {
            Items = items;
            LastPage = lastPage;
            CurrentPage = currPage;
        }

        public List<item> Items { get; set; }

        /// <summary>
        /// The last page of the catalog. Calculated by
        /// having a total number of products divided by
        /// products per page.
        /// </summary>
        public int LastPage { get; set; }

        /// <summary>
        /// The current page the user is viewing.
        /// </summary>
        public int CurrentPage { get; set; }
    }
}
