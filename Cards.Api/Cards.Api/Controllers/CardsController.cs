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
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var card = await _cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);

            return card != null ? Ok(card) : NotFound("Card not found!");
        }
        
        // Add Card
        [HttpPost]
        public async Task<ActionResult> AddCard([FromBody] Card card)
        {
            card.Id = Guid.NewGuid();

            await _cardsDbContext.Cards.AddAsync(card);
            await _cardsDbContext.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }
        
        // Updating a Card
        [HttpPut]
        [Route("id:guid")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card)
        {
            var existingCard = await _cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            
            if (existingCard == null) return NotFound("Card not found");
            
            existingCard.CardholderName = card.CardholderName;
            existingCard.CardNumber = card.CardNumber;
            existingCard.ExpiryMonth = card.ExpiryMonth;
            existingCard.ExpiryYear = card.ExpiryYear;
            existingCard.CVC = card.CVC;

            await _cardsDbContext.SaveChangesAsync();

            return Ok(existingCard);

        }
        
        
    }
}