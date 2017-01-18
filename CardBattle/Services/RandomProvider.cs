using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle.Services
{
    public class RandomProvider
    {
        private readonly Random _random = new Random();
        
        public Random Random
        {
            get
            {
                return _random;
            }
        } 
    }
}
