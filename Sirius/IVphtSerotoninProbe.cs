namespace bootstrapper.sample.Sirius
{
    using System;

    public interface IVphtSerotoninProbe
    {
        int Take();
    }

    public class VphtSerotoninProbe : IVphtSerotoninProbe
    {
        private static readonly Random randomizer = new Random();

        private readonly int[] levels = new[] { 60, 81, 30, 13, 5, 150, 30, 0 };

        public int Take()
        {
            return levels[randomizer.Next(0, 7)];
        }
    }
}