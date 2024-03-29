namespace BlazorWebAppOidc.Models.Articles
{
    public class ArticleShortDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string DescriptionN { get; set; }
        public string DescriptionF { get; set; }
        public double TotalInStock { get; set; }
        public double TotalInReservation { get; set; }
        public double TotalInOrder { get; set; }
    }
}