using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRoulette.Api.Models;

namespace TestRoulette.Api.Interfaces
{
    public interface IRouletteRepository
    {
        public Roulette GetById(string Id);

        public List<Roulette> GetAll();

        public Roulette Update(string Id, Roulette roulette);

        public Roulette Save(Roulette roulette);
    }
}
