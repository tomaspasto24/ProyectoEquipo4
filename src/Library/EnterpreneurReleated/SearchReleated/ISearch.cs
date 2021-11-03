using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public interface ISearch
    {
        List<Publication> Search(string wordToSearch);
    }
}