namespace Cyrus.Core.DomainModels
{
    public class TribeMember : BaseEntity
    {
        public bool IsAdmin { get; set; }
        public bool IsApproved { get; set; }
        public int TribeId { get; set; }
        public int UserId { get; set; }
    }

}
