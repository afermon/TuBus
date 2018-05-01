using System.Collections.Generic;
using DataAccess.Crud;
using Entities;

namespace CoreAPI
{
    public class VistaManager : BaseManager
    {
        private readonly VistaCrudFactory _crudVista;

        public VistaManager()
        {
            _crudVista = new VistaCrudFactory();
        }

        public List<Vista> RetrieveViewByRole(string roleId)
        {
            return _crudVista.RetrieveByRole<Vista>(roleId);
        }

        public Vista RetrieveByRoleAndView(Vista vista)
        {
            return _crudVista.RetrieveByRoleAndView<Vista>(vista);
        }

        public List<Vista> RetrieveAll()
        {
            return _crudVista.RetrieveAll<Vista>();
        }
    }
}
