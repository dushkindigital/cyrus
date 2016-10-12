namespace Cyrus.Core.DomainModels
{
    public class ProfileAttribute : BaseEntity
    {
        public int ProfileId { get; set; }
        public int ProfileAttributeId { get; set; }
        public string Response { get; set; }
    }
}
