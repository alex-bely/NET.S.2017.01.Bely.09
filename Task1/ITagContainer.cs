using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    /// <summary>
    /// Provides method for making a decision if the book contains given tag
    /// </summary>
    public interface ITagContainer
    {
        bool Contain(Book book);
    }
}
