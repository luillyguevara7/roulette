using System;
using System.ComponentModel.DataAnnotations;

namespace TestRoulette.Api.Models
{
    public class Bet
    {
        // position 0-36, and 37=> red, 38 => black
        [Range(0, 38)]
        public int position { get; set; }

        [Range(0.5d, maximum: 10000)]
        public double money { get; set; }

    }
}
