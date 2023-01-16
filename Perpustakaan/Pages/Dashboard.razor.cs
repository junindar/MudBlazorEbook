using ApexCharts;
using AutoMapper;
using DataService.IService;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Perpustakaan.Extensions.AutoMapper;
using Perpustakaan.Model;
using Color = MudBlazor.Color;

namespace Perpustakaan.Pages
{
    public partial class Dashboard
    {
        [Inject] public IBookService BookService { get; set; }
        private List<BookVM> _bookList = new();
        [Inject]
        public IMapper _mapper { get; set; }
      

        private async Task GetBooksAsync()
        {
            try
            {
                var result = await BookService.GetAllAsNoTrackingAsync();
                _bookList = result.Select(c => c.ToModel(_mapper)).ToList();
            }
            catch (Exception e)
            {
                _snackBar.Add(e.Message, Severity.Error);
            }


        }

      

        private ApexChartOptions<BookVM> options = new();
        private ApexChartOptions<BookVM> options2 = new();
        private ApexChartOptions<BookVM> options3 = new();
        private ApexChartOptions<BookVM> options4 = new();
        protected override async Task OnInitializedAsync()
        {
            await GetBooksAsync();
            options = new ApexChartOptions<BookVM>
            {
                Theme = new Theme
                {
                    Mode = Mode.Dark,
                    Palette = PaletteType.Palette1
                }
            };
            options2 = new ApexChartOptions<BookVM>
            {
                Theme = new Theme
                {
                    Mode = Mode.Dark,
                    Palette = PaletteType.Palette1
                }
            };
            options3 = new ApexChartOptions<BookVM>
            {
                Theme = new Theme
                {
                    Mode = Mode.Dark,
                    Palette = PaletteType.Palette1
                }
            };
            options4 = new ApexChartOptions<BookVM>
            {
                Theme = new Theme
                {
                    Mode = Mode.Dark,
                    Palette = PaletteType.Palette1
                },
                Legend=new Legend{Position=LegendPosition.Bottom}
            };
            //options2 =  options;
            //options3 = options;
            //options4 = options;
            //options2 = new ApexChartOptions<BookVM>
            //{
            //    Theme = new Theme
            //    {
            //        Mode = Mode.Dark,
            //        Palette = PaletteType.Palette1
            //    }
            //};

            await Task.CompletedTask;

        }
    }
}
