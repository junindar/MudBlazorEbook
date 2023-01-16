namespace Blazor_Introduction.Data
{
    public class Category
    {
        public int ID { get; set; }
        public string Nama { get; set; }
        public List<Book> Books { get; set; }
    }
}
