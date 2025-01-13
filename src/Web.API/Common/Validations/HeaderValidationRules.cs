namespace Web.API.Common.Validations
{
        public static class HeaderValidationRules
        {
            public static readonly IReadOnlyDictionary<string, Func<string, bool>> RequiredHeaders = new Dictionary<string, Func<string, bool>>
        {
            { "traceid", IsValidTraceId },
            { "application", IsValidApplicationHeader }
        };

            private static bool IsValidTraceId(string traceId)
            {
            Console.WriteLine(traceId);
                return Guid.TryParse(traceId, out _);
            }

            private static bool IsValidApplicationHeader(string headerValue)
            {
                return !string.IsNullOrWhiteSpace(headerValue) && headerValue.Length >= 5 && headerValue.Length <= 40;
            }
        }

}
