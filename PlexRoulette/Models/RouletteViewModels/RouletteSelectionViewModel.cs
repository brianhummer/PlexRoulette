using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlexRoulette.Models.RouletteViewModels
{
    public class RouletteSelectionViewModel
    {
        public string LibraryId { get; set; }
        public List<Metadata> WatchOptions { get; set; }
    }
}
