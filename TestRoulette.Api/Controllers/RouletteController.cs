﻿using System;
using Microsoft.AspNetCore.Mvc;
using TestRoulette.Api.Interfaces;
using TestRoulette.Api.Models;
using TestRoulette.Api.Models;
namespace TestRoulette.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouletteController : Controller
    {
        private IRouletteService _rouletteService;
        public RouletteController(IRouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }
        [HttpPost("[action]")]
        public IActionResult CreateRulette()
        {
            Roulette roulette = _rouletteService.Create();
            return Ok(new { id = roulette.Id });
        }
        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            return Ok(_rouletteService.GetAll());
        }
        [HttpPut("[action]/{id}")]
        public IActionResult OpenRoulette([FromRoute(Name = "id")] string id)
        {
            try
            {
                _rouletteService.Open(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(405);
            }
        }
        [HttpPut("[action]/{id}")]
        public IActionResult CloseRoulette([FromRoute(Name = "id")] string id)
        {
            try
            {
                Roulette roulette = _rouletteService.Close(id);
                return Ok(roulette);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(405);
            }
        }
        [HttpPost("[action]")]
        public IActionResult CreateBet([FromHeader(Name = "IdUser")] string IdUser, Bet bet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    error = true,
                    msg = "Bad Request"
                });
            }
            try
            {
                Roulette roulette = _rouletteService.CreateBet(IdUser,bet);
                return Ok(roulette);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(405);
            }
        }
    }
}