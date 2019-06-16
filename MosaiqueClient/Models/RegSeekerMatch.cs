using ZeroFormatter;

namespace Client.Models
{
    /*
      * Derived and Adapted By Justin Yanke
      * github: https://github.com/yankejustin
      * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
      * This code is created by Justin Yanke and has only been
      * modified partially.
      * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
      * Modified by StingRaptor on January 21, 2016
      * Modified by StingRaptor on March 15, 2016
      * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
      */

    [ZeroFormattable]
    public class RegSeekerMatch
    {
        [Index(0)]
        public virtual string Key { get; set; }

        [Index(1)]
        public virtual RegValueData[] Data { get; set; }

        [Index(2)]
        public virtual bool HasSubKeys { get; set; }

        public RegSeekerMatch()
        {            
        }

        public RegSeekerMatch(string key, RegValueData[] data, int subkeycount)
        {
            Key = key;
            Data = data;
            HasSubKeys = (subkeycount > 0);
        }

        public override string ToString()
        {
            return string.Format("({0}:{1})", Key, Data.ToString());
        }
    }
}
