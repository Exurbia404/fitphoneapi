using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace FitPhoneBackend.Business.Exceptions
{
    public class MissingFieldsException : Exception
    {
        public MissingFieldsException() 
            : base("Fields are missing.") { }

        public MissingFieldsException(string message) 
            : base(message) { }
    }
}