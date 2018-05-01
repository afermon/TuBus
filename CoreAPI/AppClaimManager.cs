using System.Collections.Generic;
using DataAccess.Crud;
using Entities;

namespace CoreAPI
{
    public class AppClaimManager : BaseManager
    {
        private readonly AppClaimCrudFactory _crudAppClaim;

        public AppClaimManager()
        {
            _crudAppClaim = new AppClaimCrudFactory();
        }

        public List<AppClaim> RetrieveAppClaimByRole(string roleId)
        {
            return _crudAppClaim.RetrieveByRole<AppClaim>(roleId);
        }
    }
}
