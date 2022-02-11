using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Text;

namespace Parser_Test
{

    public class Phone: IEquatable<Phone>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public double Praice { get; set; }

        public bool Equals(Phone? other)
        {
            if (other is null)
                return false;

            return this.Name == other.Name;
        }

        public override bool Equals(object obj) => Equals(obj as Phone);
        public override int GetHashCode() => (Name).GetHashCode();




    }




}
