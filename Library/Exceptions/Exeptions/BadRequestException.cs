﻿using System.Net;

namespace Exceptions.Exeptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message) : base(message, null, HttpStatusCode.BadRequest) { }
    }
}
