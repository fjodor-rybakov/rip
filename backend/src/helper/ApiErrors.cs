using System;
using System.Net;

namespace backend.helper
{
    public class Error : Exception
    {
        public int HttpStatus;
        public new string Message;
        public int Code;
    }

    public class ApiErrors
    {
        public Error ServerError => new Error
        {
            HttpStatus = (int) HttpStatusCode.InternalServerError,
            Message = "Произошла ошибка на сервере",
            Code = 1
        };
        
        public Error InvalidToken => new Error
        {
            HttpStatus = (int) HttpStatusCode.Unauthorized,
            Message = "Токен истёк или не валиден",
            Code = 2
        };

        public Error AccessDenied => new Error
        {
            HttpStatus = (int) HttpStatusCode.Forbidden,
            Message = "Недостаточно прав",
            Code = 3
        };
        
        public Error UserNotFount => new Error
        {
            HttpStatus = (int) HttpStatusCode.NotFound,
            Message = "Такой пользователь не существует. Проверьте данные для входа",
            Code = 100
        };

        public Error UserAlreadyExist => new Error
        {
            HttpStatus = (int) HttpStatusCode.BadRequest,
            Message = "Такой пользователь уже существует",
            Code = 101
        };

        public Error NewsNotFound => new Error
        {
            HttpStatus = (int) HttpStatusCode.NotFound,
            Message = "Новость не найдена",
            Code = 102
        };

        public Error CommentNotFound => new Error
        {
            HttpStatus = (int) HttpStatusCode.NotFound,
            Message = "Комментарий не найден",
            Code = 103
        };
        
        public Error FailedUploadFile => new Error
        {
            HttpStatus = (int) HttpStatusCode.ExpectationFailed,
            Message = "Неудалось загрузить файл",
            Code = 104
        };
        
        public Error FileNotFound => new Error
        {
            HttpStatus = (int) HttpStatusCode.NotFound,
            Message = "Файл не найден",
            Code = 105
        };
    }
}