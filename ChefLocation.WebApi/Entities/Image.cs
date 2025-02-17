namespace ChefLocation.WebApi.Entities
{
    public class Image : BaseTable
    {
        public int ImageId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
