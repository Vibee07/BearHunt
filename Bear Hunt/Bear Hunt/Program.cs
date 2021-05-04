using System;

namespace Bear_Hunt
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (VibeeISU game = new VibeeISU())
            {
                game.Run();
            }
        }
    }
#endif
}

