using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Fasetto.Word
{
    public class ChatListItemViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string Message { get; set; }

        public string Initials { get; set; }

        public string ProfilePictureRGB { get; set; }

        public bool NewContentAvailable { get; set; }

        public bool IsSelected { get; set; }
    }
}
