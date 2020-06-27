using System;
using System.Collections.Generic;
using TestRoulette.Api.Exceptions;
using TestRoulette.Api.Interfaces;
using TestRoulette.Api.Models;

namespace TestRoulette.Api.Services
{
    public class RouletteService : IRouletteService
    {
        private IRouletteRepository _rouletteRepository;

        public RouletteService(IRouletteRepository rouletteRepository)
        {
            _rouletteRepository = rouletteRepository;
        }

        public Roulette Create()
        {
            Roulette roulette = new Roulette()
            {
                Id = Guid.NewGuid().ToString(),
                IsOpen = false,
                OpenedAt = null,
                ClosedAt = null
            };
            _rouletteRepository.Save(roulette);

            return roulette;
        }

        public Roulette Find(string Id)
        {
            return _rouletteRepository.GetById(Id);
        }

        public Roulette Open(string Id)
        {
            Roulette roulette = _rouletteRepository.GetById(Id);
            if (roulette == null)
            {
                throw new RouletteNotFound();
            }

            if (roulette.OpenedAt != null)
            {
                throw new NotAllowedOpenException();
            }
            roulette.OpenedAt = DateTime.Now;
            roulette.IsOpen = true;

            return _rouletteRepository.Update(Id, roulette);
        }

        public Roulette Close(string Id)
        {
            Roulette roulette = _rouletteRepository.GetById(Id);
            if (roulette == null)
            {
                throw new RouletteNotFound();
            }
            if (roulette.ClosedAt != null)
            {
                throw new NotAllowedClosedException();
            }
            roulette.ClosedAt = DateTime.Now;
            roulette.IsOpen = false;

            return _rouletteRepository.Update(Id, roulette);
        }

        public Roulette Bet(string Id, string UserId, int position, double moneyBet)
        {
            if (moneyBet < 1 || moneyBet > 10000)
            {
                throw new CashOutRangeException();
            }
            Roulette roulette = _rouletteRepository.GetById(Id);
            if (roulette == null)
            {
                throw new RouletteNotFound();
            }
            if (roulette.IsOpen == false)
            {
                throw new RouletteClosedException();
            }

            double value = 0d;
            roulette.board[position].TryGetValue(UserId, out value);
            roulette.board[position].Remove(UserId + "");
            roulette.board[position].TryAdd(UserId + "", value + moneyBet);

            return _rouletteRepository.Update(roulette.Id, roulette);
        }

        public List<Roulette> GetAll()
        {
            return _rouletteRepository.GetAll();
        }
    }
}