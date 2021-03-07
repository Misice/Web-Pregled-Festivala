using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4J_Projekat.Modeli;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace Neo4J_Projekat.Servisi
{
    public class GradServis
    {
        public static string gradinstancaIme;
        public GradServis(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        public List<Grad> VratiSveGradove()
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            List<Grad> Gradovi;


            var query = new CypherQuery("match (n:Grad) return n",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            Gradovi = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Grad>(query).ToList();

            return Gradovi;
        }

        public List<Grad> VratiSveGradovePoDrzavi(string imedrzave)
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            List<Grad> gradovi;


            var query = new CypherQuery("match (n:Drzava)-[r:SADRZI]->(m:Grad) where n.ime='" + imedrzave + "' return m",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            gradovi = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Grad>(query).ToList();

            return gradovi;
        }

        public Grad VratiGradPoIme(string imegrada)
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            Grad grad;


            var query = new CypherQuery("match (n:Grad) where n.ime='" + imegrada + "' return n",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            grad = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Grad>(query).FirstOrDefault();

            return grad;
        }
    }
}
