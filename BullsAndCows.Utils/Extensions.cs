namespace BullsAndCows.Utils
{
    using System.Collections.Generic;

    public static class Extensions
    {
        public static void SortList(this List<KeyValuePair<string, int>> list)
        {
            list.Sort(SortDictionary);
        }

        private static int SortDictionary(KeyValuePair<string, int> a, KeyValuePair<string, int> b)
        {
            return a.Value.CompareTo(b.Value);
        }
    }
}
