using DataAccess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    public class ChoferManager 
    {
        private ChoferCrudFactory crudChofer;

        public ChoferManager()
        {
            crudChofer = new ChoferCrudFactory();
        }

        public void Create(Chofer chofer)
        {
            try
            {
                var c = crudChofer.Retrieve<Chofer>(chofer);

                if (c != null)
                {
                    //Chofer ya existe
                    throw new BusinessException(101);
                }

                crudChofer.Create(chofer);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public Chofer Retrieve(Chofer chofer)
        {
            Chofer c = null;
            try
            {
                c = crudChofer.Retrieve<Chofer>(chofer);

                if (c == null)
                    throw new BusinessException(103);
                
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return c;
        }

        public List<Chofer> RetrieveAll()
        {
            return crudChofer.RetrieveAll<Chofer>();
        }

        public List<Chofer> RetrieveAllRuta(int ruta)
        {
            return crudChofer.RetrieveAllRuta<Chofer>(ruta);
        }

        public void Update(Chofer chofer)
        {
            crudChofer.Update(chofer);
        }

        public void Delete(Chofer chofer)
        {
            crudChofer.Delete(chofer);
        }

        public void Activate(Chofer chofer)
        {
            try
            {
                var choferDb = crudChofer.Retrieve<Chofer>(chofer);
                if (choferDb == null)
                    throw new BusinessException(103);

                choferDb.Estado = "Activo";
                crudChofer.Update(chofer);

            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }
    }
}
