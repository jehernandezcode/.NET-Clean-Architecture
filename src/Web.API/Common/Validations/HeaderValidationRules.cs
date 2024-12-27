namespace Web.API.Common.Validations
{
        public static class HeaderValidationRules
        {
            public static readonly Dictionary<string, Func<string, bool>> RequiredHeaders = new()
        {
            { "traceid", IsValidTraceId },
            { "Application", IsValidApplicationHeader }
        };

            private static bool IsValidTraceId(string traceId)
            {
                return Guid.TryParse(traceId, out _);
            }

            private static bool IsValidApplicationHeader(string headerValue)
            {
                return !string.IsNullOrWhiteSpace(headerValue) && headerValue.Length >= 5 && headerValue.Length <= 40;
            }
        }

}
