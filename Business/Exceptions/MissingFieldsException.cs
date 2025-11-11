using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace FitphoneBackend.Business.Exceptions
{
    public class MissingFieldsException : Exception
    {
        public MissingFieldsException() 
            : base("Fields are missing.") { }

        public MissingFieldsException(string message) 
            : base(message) { }
    }
}