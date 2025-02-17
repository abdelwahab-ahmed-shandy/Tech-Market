namespace Tech_Mart.Models
{
    public class Category : BaseEntity
    {
        public string? CategoryImg { set; get; }
        public ICollection<Models.Product> products { get; set; } = new List<Product>();
    }
}
