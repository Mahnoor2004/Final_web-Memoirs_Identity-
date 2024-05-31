namespace WebApplication1.Models
{
    public class Memoirs
    {


        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public DateTime DateWritten { get; set; } = DateTime.Now;
    }
}


