using Microsoft.Win32;
using System;
using ZeroFormatter;

namespace MosaiqueServeur.Models
{
    [ZeroFormattable]
    public class RegValueData
    {
        [Index(0)]
        public virtual string Name { get; set; }

        [Index(1)]
        public virtual RegistryValueKind Kind { get; set; }

        [Index(2)]
        public virtual string Data { get; set; }

        public RegValueData()
        {
        }

        public RegValueData(string name, RegistryValueKind kind, string data)
        {
            Name = name;
            Kind = kind;
            Data = data;
        }

        public override string ToString()
        {
            return string.Format("({0}:{1}:{2})", Name, Kind, Data);
        }
    }
}
