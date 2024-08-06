using AudioBookApi.Extensions;
using AudioBookApi.Interfaces;
using AudioBookApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AudioBookApi.Controllers
{
    [Route("audiobookapi/personalpage")]
    [ApiController]
    public class PersonalPageController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;
        private readonly UserManager<User> _userManager;
        private readonly IPersonalPageRepository _personalPageRepo;
        public PersonalPageController(UserManager<User> userManager,
           IBookRepository bookRepo , IPersonalPageRepository personalPageRepo )
        {
            _userManager = userManager;
            _bookRepo = bookRepo;
            _personalPageRepo = personalPageRepo;
            
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync( username );
            var userInfo = await _personalPageRepo.GetUserInfo(user );
            return Ok(userInfo);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddInfo(string title)
        {
            var username = User.GetUsername();
            var user =await _userManager.FindByNameAsync( username );
            var book = await _bookRepo.GetByTitleAsync( title );

            if (book == null) return BadRequest("Book not found!");

            var userPage = await _personalPageRepo.GetUserInfo(user);

            if (userPage.Any(e => e.Title.ToLower() == title.ToLower())) return BadRequest("Cannot add the same book to page!");

            var pageModel = new PersonalPage
            {
                BookId = book.Id,
                UserId = user.Id,

            };
            await _personalPageRepo.CreateAsync(pageModel);

            if(pageModel == null)
            {
                return StatusCode(500, "Could not create");

            }
            else
            {
                return Created();
            }
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUserInfo(string title)
        {
            var username =User.GetUsername();

            var user = await _userManager.FindByNameAsync( username );

            var userProfile = await _personalPageRepo.GetUserInfo(user);

            var filteredBook = userProfile.Where(b=> b.Title.ToLower() == title.ToLower()); 

            if(filteredBook.Count() == 1)
            {
                await _personalPageRepo.DeleteProfileAsync(user, title);
            }
            else
            {
                return BadRequest("Book not in your library");
            }
            return Ok();
        }

    }
}
