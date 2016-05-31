using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SpotifyTrackID
{
    class Program
    {
        static string GetSpotifyTrackInfo()
        {
            var proc =
                Process.GetProcessesByName("Spotify").FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));

            // We are going to write to console, wipe last
            Console.Write("\t\t\t\r");

            if (proc == null)
            {
                Console.Write("Cannot find Spotify");
                return "";
            }

            if (string.Equals(proc.MainWindowTitle, "Spotify", StringComparison.InvariantCultureIgnoreCase))
            {

                Console.Write("Nothing playing");
                return "";
            }

            var ln = proc.MainWindowTitle;

            Console.Write("Playing: " + ln);
            return ln;

        }

        static void Main(string[] args)
        {
            var t = new Timer(1000);
            t.Elapsed += (sender, elapsedEventArgs) =>
            {
                var line = GetSpotifyTrackInfo();

                // Sadly hacky way of dealing with the return values!
                if (string.Equals(line, "")) return;

                using (StreamWriter file = new StreamWriter("currentSong.txt", false))
                {
                    file.WriteLine(line);
                }
            };

            t.Start();
            Console.ReadLine();

        }
    }
}
