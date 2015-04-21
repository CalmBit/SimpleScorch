using System;

namespace SimpleScorch
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SimpleScorch game = new SimpleScorch())
            {
                game.Run();
            }
        }
    }
#endif
}

