using DataAccess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    public class TerminalManager
    {
        private readonly TerminalCrudFactory _crudTerminal;

        public TerminalManager()
        {
            _crudTerminal = new TerminalCrudFactory();
        }

        public void Create(Terminal terminal)
        {
            try
            {
                var t = _crudTerminal.RetrieveByName<Terminal>(terminal);

                if (t != null)
                {
                    //Terminal ya existe
                    throw new BusinessException(100);
                }
                                
                _crudTerminal.Create(terminal);

                t = _crudTerminal.RetrieveByName<Terminal>(terminal);
                var configManager = new ConfiguracionManager();
                configManager.CreateConfiguracionTerminal(t.Id);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Terminal> RetrieveAll()
        {
            return _crudTerminal.RetrieveAll<Terminal>();
        }

        public Terminal RetrieveByName(Terminal terminal)
        {

            return _crudTerminal.RetrieveByName<Terminal>(terminal);
        }

        public Terminal RetrieveById(Terminal terminal)
        {
            return _crudTerminal.Retrieve<Terminal>(terminal);
        }

        public void Update(Terminal terminal)
        {
            _crudTerminal.Update(terminal);
        }

        public void Delete(Terminal terminal)
        {
            _crudTerminal.Delete(terminal);
        }

        public void Activate(Terminal terminal)
        {
            try
            {
                var terminalDb = _crudTerminal.Retrieve<Terminal>(terminal);
                if (terminalDb == null)
                    throw new BusinessException(102);

                terminalDb.Estado = "Activo";
                _crudTerminal.Update(terminal);

            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }
    }
}
