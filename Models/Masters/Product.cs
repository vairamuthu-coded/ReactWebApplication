using System.ComponentModel.DataAnnotations;

namespace ReactWebApplication.Models.Masters
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; } 
        public string Author { get; set; }  
        public int ProductId { get; set; } = 0;
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }  
        public int ProductCategoryId { get; set;} = 0;
        public string ProductCategoryName { get; set;}
        public string ProductCategoryDescription { get; set;}
        public string ProductCategoryAuthor { get; set;}   
            
            
    }
}
