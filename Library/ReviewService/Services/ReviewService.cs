﻿using AutoMapper;
using Exceptions.Exeptions;
using ReviewService.DTOs;
using ReviewService.Enums;
using ReviewService.Interfaces;
using ReviewService.Model;

namespace ReviewService.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;
        private readonly IRabbitMQProducerService _rabbitmqProducerService;
        private readonly ICheckBookService _checkBookService;

        public ReviewService(IReviewRepository repository, IMapper mapper, IRabbitMQProducerService rabbitmqProducerService,
            ICheckBookService checkBookService)
        {
            _repository = repository;
            _mapper = mapper;
            _rabbitmqProducerService = rabbitmqProducerService;
            _checkBookService = checkBookService;
        }

        public async Task CreateReview(string username, string email, CreateReviewDTO newReview)
        {
            try
            {
                await _checkBookService.CheckBook(newReview.BookId);
            }
            catch (NotFoundException nfe)
            {
                throw nfe;
            }
            catch (CustomException ce)
            {
                throw ce;
            }

            var review = _mapper.Map<Review>(newReview);

            review.Username = username;
            review.Email = email;

            await _repository.Insert(review);
            await _repository.Save();

            _rabbitmqProducerService.SendMailRequest("Verify Review", "You just made review. Wait for administration to verify it.", email);
        }

        public async Task EditReview(int id, string username, EditReviewDTO newReviewInfo)
        {
            var reviewQuery = await _repository.GetAllAsync();
            var review = reviewQuery.Where(x => x.Id == id && x.Username.Equals(username) && x.Verified != Enums.ReviewVerifiedState.Denied).FirstOrDefault()
                ?? throw new NotFoundException(string.Format("There is no review with id: {0} and username: {1}", id, username));

            review.Verified = ReviewVerifiedState.Waiting;

            _mapper.Map<EditReviewDTO, Review>(newReviewInfo, review);

            _repository.Update(review);
            await _repository.Save();

            _rabbitmqProducerService.SendMailRequest("Verify Review", "You just edit review. Wait for administration to verify it.", review.Email);
        }

        public async Task<List<ReviewDTO>> GetAllMyReviews(string username)
        {
            var reviewQuery = await _repository.GetAllAsync();
            var reviews = reviewQuery.Where(x => x.Username.Equals(username)).ToList();

            return _mapper.Map<List<ReviewDTO>>(reviews);
        }

        public async Task<List<ReviewDTO>> GetUnverifiedReviewsForBook(int bookId)
        {
            var reviewQuery = await _repository.GetAllAsync();
            var review = reviewQuery.Where(x => x.BookId == bookId && x.Verified == Enums.ReviewVerifiedState.Waiting).ToList();

            return _mapper.Map<List<ReviewDTO>>(review);
        }

        public async Task<List<ReviewDTO>> GetVerifiedReviewsForBook(int bookId)
        {
            var reviewQuery = await _repository.GetAllAsync();
            var review = reviewQuery.Where(x => x.BookId == bookId && x.Verified == Enums.ReviewVerifiedState.Accepted).ToList();

            return _mapper.Map<List<ReviewDTO>>(review);
        }

        public async Task VerifyReview(int id, VerifyReviewDTO verifyReview)
        {
            var reviewQuery = await _repository.GetAllAsync();
            var review = reviewQuery.Where(x => x.Id == id && x.Verified == Enums.ReviewVerifiedState.Waiting).FirstOrDefault()
                ?? throw new NotFoundException(string.Format("There is no review with id: {0}", id));

            review.Verified = (ReviewVerifiedState)Enum.Parse(typeof(ReviewVerifiedState), verifyReview.VerifiedState);

            _repository.Update(review);
            await _repository.Save();

            _rabbitmqProducerService.SendMailRequest("Verify Review", string.Format("Your comment is verified with state: {0}", verifyReview.VerifiedState), review.Email);
        }
    }
}
