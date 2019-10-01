using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace backend.helper
{
    public class Error : Exception
    {
        public int HttpStatus;
        public string Message;
    }

    public class ApiErrors
    {
        public Error UserNotFount => new Error
        {
            HttpStatus = (int) HttpStatusCode.NotFound,
            Message = "Такой пользователь не существует. Проверьте данные для входа"
        };

        public Error UserAlreadyExist => new Error
            {HttpStatus = (int) HttpStatusCode.BadRequest, Message = "Такой пользователь уже существует"};

        public Error ServerError => new Error
            {HttpStatus = (int) HttpStatusCode.InternalServerError, Message = "Произошла ошибка на сервере"};

        public Error InvalidToken => new Error
            {HttpStatus = (int) HttpStatusCode.Unauthorized, Message = "Токен истёк или не валиден"};

        public Error AccessDenied => new Error
        {
            HttpStatus = (int) HttpStatusCode.Forbidden, Message = "Недостаточно прав"
        };
    }
}