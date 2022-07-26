using System;
namespace Walkland.Core.Models
{
    public class CompanySubCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CompanyCategoryId { get; set; }

        public string CompanySubCategoryStoragePath { get; set; }
    }
}
