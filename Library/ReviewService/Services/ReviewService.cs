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

        public ReviewService(IReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateReview(string username, CreateReviewDTO newReview)
        {
            // Maybe check if there is that book

            var review = _mapper.Map<Review>(newReview);

            await _repository.Insert(review);
            await _repository.Save();
        }

        public async Task EditReview(int id, string username, EditReviewDTO newReviewInfo)
        {
            var reviewQuery = await _repository.GetAllAsync();
            var review = reviewQuery.Where(x => x.Id == id && x.Username.Equals(username) && x.Verified != Enums.ReviewVerifiedState.Denied).FirstOrDefault()
                ?? throw new NotFoundException(string.Format("There is no review with id: {0} and username: {1}", id, username));

            _mapper.Map<EditReviewDTO, Review>(newReviewInfo, review);

            _repository.Update(review);
            await _repository.Save();
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

            //Here communication with email service
        }
    }
}