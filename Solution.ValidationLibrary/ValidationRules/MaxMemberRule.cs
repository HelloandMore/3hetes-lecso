namespace Solution.ValidationLibrary.ValidationRules
{
    public class MaxMembersRule<T> : IValidationRule<List<T>>
    {
        public string ValidationMessage { get; set; }

        public bool Check(object value)
        {
            var members = value as List<T>;
            return members != null && members.Count <= 10;
        }
    }
}
