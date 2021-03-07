using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Neo4J_Projekat.Modeli;
using Neo4jClient;
using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neo4J_Projekat.Servisi
{
    public class KontinentServis
    {
        public static string kontinentinstancaIme;
        public KontinentServis(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }


        public List<Kontinent> VratiSveKontinente()
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            List<Kontinent> kontinenti;


            var query = new CypherQuery("match (n:Kontinent) return n",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            kontinenti = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Kontinent>(query).ToList();

            return kontinenti;
        }

        public Kontinent VratiKontinentPoIme(string imekontinenta)
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            Kontinent kontinent;


            var query = new CypherQuery("match (n:Kontinent) where n.ime='" + imekontinenta + "' return n",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            kontinent = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Kontinent>(query).FirstOrDefault();

            return kontinent;
        }
    }
}
