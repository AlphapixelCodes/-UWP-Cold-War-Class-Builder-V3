using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cold_War_Class_Builder_V3
{
    public static class Links
    {
        public static void OpenLink(string url)
        {
            var success = Windows.System.Launcher.LaunchUriAsync(new Uri(url));
        }
        public static string Icons8 = "http://www.icons8.com",
            YoutubeChannel="https://www.youtube.com/channel/UCyTsi1Tja0kkDv26gX5CjxQ";
    }
}
