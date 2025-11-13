using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace FitPhoneBackend.Business.Exceptions
{
    public class InvalidUserException : Exception
    {
        public InvalidUserException() 
            : base("Invalid User.") { }

        public InvalidUserException(string message) 
            : base(message) { }
    }
}