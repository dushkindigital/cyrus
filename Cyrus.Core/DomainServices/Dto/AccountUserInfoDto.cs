namespace Cyrus.Core.DomainServices.Dto
{
    public class AccountUserInfoDto
    {
        // We make all of these elements match AspNetUsers, so AutoMapper has a nice time projecting onto it
        public int UserId { get; set; }
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public bool HasRegistered { get; set; }
        public string LoginProvider { get; set; }
        
        // Uses AutoMapper feature of (Course.Title) will match to (CourseTitle)
        // public string CourseTitle { get; set; }
    }
}
