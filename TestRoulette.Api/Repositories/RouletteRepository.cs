using EasyCaching.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using TestRoulette.Api.Interfaces;
using TestRoulette.Api.Models;
namespace TestRoulette.Api.Repositories
{
    public class RouletteRepository : IRouletteRepository
    {
        private IEasyCachingProviderFactory _cachingProviderFactory;
        private IEasyCachingProvider _cachingProvider;
        private const string Key = "tblRoulette";
        private static string Connection = Environment.GetEnvironmentVariable("Connection");
        public RouletteRepository(IEasyCachingProviderFactory cachingProviderFactory)
        {
            _cachingProviderFactory = cachingProviderFactory;
            _cachingProvider = _cachingProviderFactory.GetCachingProvider(Connection);
        }
        public Roulette GetById(string Id)
        {
            var item = _cachingProvider.Get<Roulette>(Key + Id);
            if (!item.HasValue)
            {
                return null;
            }
            return item.Value;
        }
        public List<Roulette> GetAll()
        {
            var rouletes = _cachingProvider.GetByPrefix<Roulette>(Key);
            if (rouletes.Values.Count == 0)
            {
                return new List<Roulette>();
            }
            return new List<Roulette>(rouletes.Select(x => x.Value.Value));
        }
        public Roulette Update(string Id, Roulette roulette)
        {
            roulette.Id = Id;
            return Save(roulette);
        }
        public Roulette Save(Roulette roulette)
        {
            _cachingProvider.Set(Key + roulette.Id, roulette, TimeSpan.FromDays(365));
            return roulette;
        }
    }
}
