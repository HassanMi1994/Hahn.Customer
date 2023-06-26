namespace Hahn.Customers.Infrastructure.FluentValidations
{
    public class CustomValidationException : Exception
    {
        public Dictionary<string, string> FlatErrors { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
        public CustomValidationException(Dictionary<string, string[]> errors)
        {

            Errors = errors;

            foreach (var error in errors)
            {
                string errorValues = "";
                foreach (var item in error.Value)
                {
                    errorValues += item.ToString() + '\n';
                }
                FlatErrors.Add(System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(error.Key), errorValues);
            }
        }

        public CustomValidationException(string title, string message)
        {
            FlatErrors.Add(title, message);
        }
    }
}
