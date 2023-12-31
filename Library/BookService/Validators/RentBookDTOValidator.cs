﻿using BookService.DTO;
using FluentValidation;

namespace BookService.Validators
{
    public class RentBookDTOValidator : AbstractValidator<RentBookDTO>
    {
        public RentBookDTOValidator()
        {
            RuleFor(r => r.BookId).NotEmpty();
        }
    }
}
