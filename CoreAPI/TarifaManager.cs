using System;
using DataAccess.Crud;
using Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using Exceptions;

namespace CoreAPI
{
    public class TarifaManager : BaseManager
    {
        private readonly TarifaCrudFactory _crudTarifa;

        public TarifaManager()
        {
            _crudTarifa = new TarifaCrudFactory();
        }

        public List<Tarifa> RetrieveAll()
        {
            return _crudTarifa.RetrieveAll<Tarifa>();
        }

        public List<Tarifa> RetrieveEmpresarios()
        {
            return _crudTarifa.RetrieveEmpresarios<Tarifa>();
        }

        public Tarifa Retrieve(Tarifa tarifa)
        {
            Tarifa tarifaDb = null;
            try
            {
                tarifaDb = _crudTarifa.Retrieve<Tarifa>(tarifa);

                if (tarifaDb == null)
                    throw new BusinessException(212);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return tarifaDb;
        }

        public void UpdateTarifasAresep()
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(ConfigurationManager.AppSettings["AresepFareEndpoint"])
                };

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                var response = client.GetAsync("").Result;

                if (!response.IsSuccessStatusCode)
                    throw new BusinessException(214);

                var dataResponse = response.Content.ReadAsStringAsync().Result;
                // response.Content.ReadAsAsync<AresepApiResponse>().Result;
                var apiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<AresepApiResponse>(dataResponse);
                var tarifas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Tarifa>>(apiResponse.Bus_Routes.ToString()).Where(t => t.RouteName.Contains("SAN JOSE")).ToList();
                foreach (var tarifa in tarifas)
                    _crudTarifa.Create(tarifa);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }


        }
    }
}
