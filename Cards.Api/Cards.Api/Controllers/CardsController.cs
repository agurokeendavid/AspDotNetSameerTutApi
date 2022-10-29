using Cards.Api.Data;
using Cards.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cards.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardsDbContext _cardsDbContext;
        public CardsController(CardsDbContext cardsDbContext)
        {
            _cardsDbContext = cardsDbContext;
        }
        
        // Get All Cards
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await _cardsDbContext.Cards.ToListAsync();
            
            return Ok(cards);
        }
        
        // Get Single Card
        [HttpGet]
        [Route("id:guid")]
        public async Task<IActionResult> GetAllCards([FromRoute] Guid id)
        {
            var card = await _cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);

            return card != null ? Ok(card) : NotFound("Card not found!");
        }
    }
}