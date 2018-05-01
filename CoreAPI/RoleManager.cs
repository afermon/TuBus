using DataAccess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreAPI
{
    public class RoleManager : BaseManager
    {
        private readonly RoleCrudFactory _crudRole;
        private readonly VistaCrudFactory _crudVista;

        public RoleManager()
        {
            _crudRole = new RoleCrudFactory();
            _crudVista = new VistaCrudFactory();
        }

        public Role RetrieveById(Role role)
        {
            role = _crudRole.Retrieve<Role>(role);
            role.Vistas = _crudVista.RetrieveByRole<Vista>(role.RoleId);

            return role;
        }

        public Role RetrieveViewsById(Role role)
        {
            role = _crudRole.RetrieveViews<Role>(role);

            return role;
        }

        public List<Role> RetrieveAll()
        {
            return _crudRole.RetrieveAll<Role>();
        }

        public void Create(Role rol)
        {
            try
            {
                var rolPorNombre = _crudRole.RetrieveByName<Role>(rol);

                if (rolPorNombre != null)
                    throw new BusinessException(400);

                _crudRole.Create(rol);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void AddViewToRol(Role rol)
        {
            try
            {
                if (rol.Vistas.Any())
                {
                    var vistaManager = new VistaManager();

                    foreach (var v in rol.Vistas)
                    {

                        var vista = new Vista
                        {
                            VistaId = v.VistaId,
                            RoleId = rol.RoleId
                        };

                        var vistaPorRol = vistaManager.RetrieveByRoleAndView(vista);

                        if (vistaPorRol != null)
                            throw new BusinessException(403);

                        else
                        {
                            _crudVista.Create(vista);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Update(Role rol)
        {
            _crudRole.Update(rol);
        }

        public void Disable(Role rol)
        {
            _crudRole.Disable(rol);
        }
    }
}