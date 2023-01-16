using Microsoft.AspNetCore.Components;

namespace Perpustakaan.Shared.Components
{
    public partial class NoResultBox
    {
        [Parameter]
        public string SearchParameter { get; set; }

        [Parameter]
        public string StyleParameter { get; set; }
    }
}
