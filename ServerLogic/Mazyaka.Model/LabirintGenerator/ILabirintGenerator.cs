using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mazyaka.Model.StructLabirint;

namespace Mazyaka.Model.LabirintGenerator
{
    public interface ILabirintGenerator
    {
        Cell[,] Generate(int size);
    }
}