using System.Collections.Generic;

namespace Cyrus.Core.DomainModels
{
    public class Tribe : BaseEntity    {

        //public Tribe()
        //{
        //    Members = new List<TribeMember>();
            
        //}

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }

        #region navigation properties
        public virtual ICollection<TribeMember> Members { get; set; }
        #endregion

    }
}
