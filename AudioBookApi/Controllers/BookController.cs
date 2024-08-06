using AudioBookApi.Data;
using AudioBookApi.Dtos.Book;
using AudioBookApi.Helpers;
using AudioBookApi.Interfaces;
using AudioBookApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AudioBookApi.Controllers
{
    [Route("audiobookapi/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookRepository _bookRepository;
        public BookController(ApplicationDbContext context,IBookRepository bookRepository )
        {

            _context = context;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [Authorize]
        public  async  Task<IActionResult> GetAll([FromQuery]QueryObject query)
           
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var books = await _bookRepository.GetAllAsync(query);
             var bookDto = books.Select(b => b.ToBookDto());
            return Ok(books);

        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var book = await _bookRepository.GetByIdAsync(id);

            if(book == null)
            {
                return NotFound();
            }
            return Ok(book.ToBookDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateBookRequestDto bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var bookModel = bookDto.ToBookFromCreateDto();
            await _bookRepository.CreateAsync(bookModel);
          
            return CreatedAtAction(nameof(GetById), new {id = bookModel.Id},bookModel.ToBookDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var bookModel = await _bookRepository.UpdateAsync(id, updateDto);
            if(bookModel == null)
            {
                return NotFound();
            }
        

            return Ok(bookModel.ToBookDto());   

        }
        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var bookModel =await _bookRepository.DeleteAsync(id);
             if(bookModel == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }

    }
}
