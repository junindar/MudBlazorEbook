using Blazor_Introduction.Data;
using Microsoft.AspNetCore.Components;

namespace Blazor_Introduction.Pages
{
    public class BookDetailBase : ComponentBase
    {
        [Parameter]
        public string BookId { get; set; }

        public Book Book { get; set; } = new Book();

        protected override Task OnInitializedAsync()
        {

            InitializeCategories();
            InitializeBooks();

            Book = Books.FirstOrDefault(e => e.ID == int.Parse(BookId));
            var cat = Categories.First(c => c.ID == Book.CategoryID);
            Book.Category = cat;
            return base.OnInitializedAsync();
        }
        public IEnumerable<Book> Books { get; set; }
        private List<Category> Categories { get; set; }

        private void InitializeCategories()
        {
            Categories = new List<Category>()
            {
                new Category{ID = 1, Nama = "Novel"},
                new Category{ID = 2, Nama = "Cerpen"},
                new Category{ID = 3, Nama = "Fiksi"},
                new Category{ID = 4, Nama = "Komputer"},
                new Category{ID = 5, Nama = "Sosial"},


            };
        }

        private void InitializeBooks()
        {
            var book1 = new Book()
            {
                ID = 1,
                Judul = "Visual Studio LightSwitch - Edisi Bundling",
                Penulis = "Junindar",
                Penerbit = "EBOOKUID",
                Deskripsi = "",
                Status = true,
                Gambar = "Ls4.jpg",
                CategoryID = 4

            };

            var book2 = new Book
            {
                ID = 2,
                Judul = "XAMARIN ANDROID - Mudah Membangung Aplikasi Mobile",
                Penulis = "Junindar",
                Penerbit = "EBOOKUID",
                Deskripsi = "",
                Status = true,
                Gambar = "xamarin-android.jpg",
                CategoryID = 4
            };

            var book3 = new Book
            {
                ID = 3,
                Judul = "Raga Kayu Jiwa Manusia",
                Penulis = "Sarah Anais Andrieu",
                Penerbit = "Kepustakaan Populer Gramedia",
                Deskripsi = @"Wayang golek purwa kini sangat populer di Tanah Sunda, Jawa Barat, Indonesia. 
                            Praktik yang kompleks dalam dimensi sosial dan artistiknya ini diproklamasikan oleh UNESCO 
                            sebagai Karya Agung Warisan Budaya Lisan dan Takbenda Manusia yang merupakan bagian dari pencalonan umum “Wayang Indonesia”, pada tahun 2003.
                            Buku ini menguraikan dan membahas jalur yang dilalui suatu warisan keluarga hingga menjadi suatu warisan bersama, nasional, dan dunia. 
                            Analisis antropologi ini memadukan kajian politik budaya di tingkat-tingkat tersebut dengan 
                            kajian konsep-konsep global dan studi mendalam mengenai tahapan pencalonan pertama Indonesia pada warisan takbenda UNESCO, 
                            serta realitas etnografi wayang golek. Dari proses warisanisasi resmi (yaitu proses menjadi warisan) itu muncul banyak kepentingan, 
                            seperti pembentukan identitas dan budaya nasional, atau pula spektakularisasi dan folklorisasi wayang golek, perubahannya menjadi sebuah produk ekspor, suatu sumber daya untuk digerakkan dan didayagunakan.",
                Status = true,
                CategoryID = 5,
                Gambar = "sosial1.jpg"
            };
            var book4 = new Book
            {
                ID = 4,
                Judul = "Generasi Phi",
                Penulis = "Dr.Muhammad Faizal",
                Penerbit = "Republika Penerbit",
                Deskripsi = "",
                Status = true,
                CategoryID = 5,
                Gambar = "sosial2.jpg"
            };

            Books = new List<Book> { book1, book2, book3, book4 };
        }

    }
}
