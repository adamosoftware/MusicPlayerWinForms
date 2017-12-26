using MusicPlayer.Models;
using System;

namespace ConsoleTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var db = new Mp3Database(@"C:\Users\Adam\OneDrive\Music");
            IProgress<string> progress = new Progress<string>(ShowProgress);
            db.FillAsync(progress).Wait();
        }

        private static void ShowProgress(string obj)
        {
            Console.WriteLine(obj);
        }
    }
}