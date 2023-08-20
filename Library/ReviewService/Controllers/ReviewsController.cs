using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewService.DTOs;
using ReviewService.Interfaces;

namespace ReviewService.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize]
        [HttpGet("verified/{bookId:int}")]
        public async Task<ActionResult<List<ReviewDTO>>> GetVerifiedByBookId(int bookId)
        {
            var result = await _reviewService.GetVerifiedReviewsForBook(bookId);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("unverified/{bookId:int}")]
        public async Task<ActionResult<List<ReviewDTO>>> GetUnverifiedByBookId(int bookId)
        {
            var result = await _reviewService.GetUnverifiedReviewsForBook(bookId);

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> Create(CreateReviewDTO newReview)
        {
            var username = User.Claims.First(c => c.Type == "Username").Value;
            await _reviewService.CreateReview(username, newReview);

            return Ok(string.Format("Successfully made comment from user: {0}", username));
        }

        [Authorize]
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<string>> Edit(int id, EditReviewDTO newReviewInfo)
        {
            var username = User.Claims.First(c => c.Type == "Username").Value;
            await _reviewService.EditReview(id, username, newReviewInfo);

            return Ok(string.Format("Successfully made comment from user: {0}", username));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("verify/{id:int}")]
        public async Task<ActionResult<string>> Verify(int id, VerifyReviewDTO verifyReview)
        {
            await _reviewService.VerifyReview(id, verifyReview);

            return Ok(string.Format("Successfully verified review with id:{0} to state: {1}", id, verifyReview.VerifiedState));
        }
    }
}
