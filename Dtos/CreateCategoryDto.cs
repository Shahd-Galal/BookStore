namespace BookStoreApi.Dtos
{
    public class CreateCategoryDto
    {
        internal int id;

        [MaxLength (length:100)]
        public string Name { get; set; }

    }
}
