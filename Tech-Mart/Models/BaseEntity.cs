namespace Tech_Mart.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }

        [MinLength(4)]
        [MaxLength(100)]
        public string Name { get; set; }

        [MinLength(10)]
        [MaxLength(450)]
        public string? Description { get; set; }

    }
}
