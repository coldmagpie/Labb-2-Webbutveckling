using System.Net.Http.Json;
using DataAccess.DataAccess.Models;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

        public event Action OnChange;

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CategoryDto>>("categories");

            Categories = result
                //.Select(p => new CategoryDto
            //{
            //    Id = p.Id,
            //    Name = p.Name
            //})
            .ToList();
            OnChange?.Invoke();
            return Categories;
        }

    }
}

