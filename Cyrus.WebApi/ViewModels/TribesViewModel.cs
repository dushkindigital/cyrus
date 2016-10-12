using System.Collections.Generic;

namespace Cyrus.WebApi.ViewModels
{
    public class TribesViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<TribeMembersViewModel> Enrollments { get; set; }
    }

    public class TribeMembersViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}