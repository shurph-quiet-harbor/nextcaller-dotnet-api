namespace NextCallerApi.Entities.Common
{
    /// <summary>
    /// Represents HTTP header as pair of strings (Name and Value).
    /// </summary>
    public class Header
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
