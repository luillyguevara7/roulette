using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRoulette.Api.Models;

namespace TestRoulette.Api.Interfaces
{
    public interface IRouletteService
    {
        public Roulette Create();

        public Roulette Find(string Id);

        public Roulette Open(string Id);

        public Roulette Close(string Id);

        public Roulette Bet(string Id, string UserId, int position, double money);

        public List<Roulette> GetAll();
    }
}
