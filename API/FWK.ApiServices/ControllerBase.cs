using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FWK.AppService.Interface;
using FWK.Domain;
using FWK.Domain.Interfaces.Services;
using System.Net;

namespace FWK.ApiServices
{
    public abstract class ControllerBase : Controller
    {
 
        protected readonly IAuthService authService;

        protected ControllerBase()
        {
             authService = ServiceProviderResolver.ServiceProvider.GetService<IAuthService>();
        }

        private IActionResult ReturnError<T>(String message, string stackTrace, ActionStatus status = ActionStatus.Error)
        {
            return this.ReturnError<T>(new List<string>() { message.ToString() }, stackTrace, status);
        }

        private IActionResult ReturnError<T>(List<String> message, string stackTrace, ActionStatus status = ActionStatus.Error)
        {
            var userName = authService.GetCurretUserName();
            var sessionId = authService.GetSessionID();
            //logger.Log(new LogDto()
            //{
            //    LogDate = DateTime.Now,
            //    LogMessage = String.Join(",", message),
            //    LogType = LogType.Error,
            //    LogLevel = status == ActionStatus.Error ? Domain.Entities.LogLevel.Error : Domain.Entities.LogLevel.Warning,
            //    SessionId = sessionId,
            //    UserName = userName,
            //    StackTrace = stackTrace
            //});
            return this.ReturnData<T>(default(T), status, message);
        }


        private Model.ResponseModel<T> ResponseModelError<T>(String message, string stackTrace, ActionStatus status = ActionStatus.Error)
        {
            return this.ResponseModelError<T>(new List<string>() { message.ToString() }, stackTrace, status);
        }


        private Model.ResponseModel<T> ResponseModelError<T>(List<String> message, string stackTrace, ActionStatus status = ActionStatus.Error)
        {
            var userName = authService.GetCurretUserName();
            var sessionId = authService.GetSessionID();
            //logger.Log(new LogDto()
            //{
            //    LogDate = DateTime.Now,
            //    LogMessage = String.Join(",", message),
            //    LogType = LogType.Error,
            //    LogLevel = status == ActionStatus.Error ? Domain.Entities.LogLevel.Error : Domain.Entities.LogLevel.Warning,
            //    SessionId = sessionId,
            //    UserName = userName,
            //    StackTrace = stackTrace
            //});
            return this.ResponseModel<T>(default(T), status, message);
        }






        protected IActionResult ReturnError<T>(Exception ex)
        {

            if (ex is ValidationException)
            {
                return this.ReturnValidationError<T>(ex as ValidationException);
            }
            else if (ex is BaseException)
            {
                return this.ReturnWarningError<T>(ex as BaseException);
            }

            return this.ReturnError<string>(ex.Message, ex.StackTrace);
        }

        private IActionResult ReturnValidationError<T>(ValidationException ex)
        {
            return this.ReturnError<string>(ex.Message, ex.StackTrace, ActionStatus.ValidationError);
        }

        private IActionResult ReturnWarningError<T>(BaseException ex)
        {
            return this.ReturnError<string>(ex.Message, ex.StackTrace, ActionStatus.Warning);
        }


        private Model.ResponseModel<T> ResponseModelValidationError<T>(ValidationException ex)
        {
            return this.ResponseModelError<T>(ex.Message, ex.StackTrace, ActionStatus.ValidationError);
        }

        private Model.ResponseModel<T> ResponseModelWarningError<T>(BaseException ex)
        {
            return this.ResponseModelError<T>(ex.Message, ex.StackTrace, ActionStatus.Warning);
        }



        protected Model.ResponseModel<T> ResponseModel<T>(T objectData, ActionStatus status = ActionStatus.Ok, List<String> messages = null)
        {
            var objectReturn = new Model.ResponseModel<T>()
            {
                DataObject = objectData,
                Status = status.ToString(),
                Messages = messages ?? new List<string>()
            };

            if (status == ActionStatus.Error)
            {
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            if (status == ActionStatus.ValidationError)
            {
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            if (status == ActionStatus.Warning)
            {
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            return objectReturn;

        }


        protected IActionResult ReturnData<T>(T objectData, ActionStatus status = ActionStatus.Ok, List<String> messages = null)
        {
            var objectReturn = new Model.ResponseModel<T>()
            {
                DataObject = objectData,
                Status = status.ToString(),
                Messages = messages ?? new List<string>()
            };

            if (status == ActionStatus.Error)
            {
                return this.NotFound(objectReturn);
            }
            if (status == ActionStatus.ValidationError)
            {
                return this.NotFound(objectReturn);
            }
            if (status == ActionStatus.Warning)
            {
                return this.NotFound(objectReturn);
            }
            else
            {
                return Ok(objectReturn);
            }

        }
        protected IActionResult ReturnError<T>(ModelStateDictionary ModelState)
        {
            var messages = ModelState.Values.SelectMany(e => e.Errors.Select(GetMessagesModelState)).ToList();

            return this.ReturnError<T>(messages, Newtonsoft.Json.JsonConvert.SerializeObject(ModelState), ActionStatus.ValidationError);
        }


        protected Model.ResponseModel<T> ResponseModelError<T>(Exception ex)
        {

            if (ex is ValidationException)
            {
                return this.ResponseModelValidationError<T>(ex as ValidationException);
            }
            else if (ex is BaseException)
            {
                return this.ResponseModelWarningError<T>(ex as BaseException);
            }

            return this.ResponseModelError<T>(ex.Message, ex.StackTrace);
        }

        protected Model.ResponseModel<T> ResponseModelError<T>(ModelStateDictionary ModelState)
        {
            var messages = ModelState.Values.SelectMany(e => e.Errors.Select(GetMessagesModelState)).ToList();

            return this.ResponseModelError<T>(messages, Newtonsoft.Json.JsonConvert.SerializeObject(ModelState), ActionStatus.ValidationError);
        }

        private string GetMessagesModelState(ModelError e)
        {
            return !string.IsNullOrEmpty(e.ErrorMessage) ? e.ErrorMessage : e.Exception?.Message;
        }
    }
}
