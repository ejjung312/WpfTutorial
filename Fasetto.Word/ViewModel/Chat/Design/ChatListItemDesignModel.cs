using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word
{
    public class ChatListItemDesignModel : ChatListItemViewModel
    {
        public static ChatListItemDesignModel Instance { get; } = new ChatListItemDesignModel();

        public ChatListItemDesignModel()
        {
            Initials = "AA";
            Name = "Luke";
            Message = "This chat app is awesome! I bet it will be fast too";
            ProfilePictureRGB = "3099c5";
        }
    }
}
