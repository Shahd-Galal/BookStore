namespace BookStoreApi.Dtos
{
    public class CreateCategoryDto
    {
        [MaxLength (length:100)]
        public string Name { get; set; }

    }
}
