namespace Project.MVCUI.Areas.Admin.AdminViewModels.AdminWrapperClasses
{
    public class CategoryWrapper
    {
        public CategoryViewModel? Category { get; set; }
        public ICollection<CategoryViewModel>? Categories { get; set; }
    }
}
