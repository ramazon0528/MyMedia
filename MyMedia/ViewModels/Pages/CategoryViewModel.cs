using System.Collections.ObjectModel;
using MyMedia.AppLayer.Services;
using MyMedia.Commands;
using MyMedia.Domain.Entities;

namespace MyMedia.ViewModels.Pages;

public class CategoryViewModel : ViewModelBase
{
    private readonly CategoryService _categoryService;

    public CategoryViewModel(CategoryService categoryService)
    {
        _categoryService = categoryService;

        LoadCategoriesCommand = new(LoadCategoriesAsync);
        AddCategoryCommand = new(AddCategoryAsync);
        DeleteCategoryCommand = new(DeleteCategoryAsync);
        EditCategoryCommand = new(EditCategoryAsync);
    }

    private async Task LoadCategoriesAsync()
    {
        Categories.Clear();

        var categories = await _categoryService.GetAllAsync();

        foreach (var item in categories)
            Categories.Add(item);
    }

    private async Task AddCategoryAsync()
    {
        if (string.IsNullOrEmpty(CategoryName))
            return;

        var category = new Category() { Name = CategoryName };

        await _categoryService.AddAsync(category);
        await LoadCategoriesAsync();

        CategoryName = string.Empty;
    }

    private async Task EditCategoryAsync()
    {
        if (SelectedCategory == null)
            return;

        await _categoryService.EditAsync(SelectedCategory);
        await LoadCategoriesAsync();
        SelectedCategory = null!;
    }

    private async Task DeleteCategoryAsync()
    {
        if (SelectedCategory == null)
            return;

        await _categoryService.DeleteAsync(SelectedCategory.Id);
        await LoadCategoriesAsync();
    }

    public ObservableCollection<Category> Categories { get; set; } = [];
    public Category SelectedCategory { get; set; } = null!;
    public string CategoryName { get; set; } = string.Empty;

    public AsyncRelayCommand LoadCategoriesCommand { get; }
    public AsyncRelayCommand AddCategoryCommand { get; }
    public AsyncRelayCommand DeleteCategoryCommand { get; }
    public AsyncRelayCommand EditCategoryCommand { get; set; }
}
