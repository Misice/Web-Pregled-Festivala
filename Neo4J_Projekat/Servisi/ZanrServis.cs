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
    public class ZanrServis
    {
        public ZanrServis(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        public static string zanrinstancaIme;

        public IWebHostEnvironment WebHostEnvironment { get; }

        public List<Zanr> VratiSveZanrove()
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            List<Zanr> Zanrovi;


            var query = new CypherQuery("match (n:Zanr) return n",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            Zanrovi = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Zanr>(query).ToList();

            return Zanrovi;
        }

        public Zanr VratiZanrPoIme(string imeZanra)
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


           Zanr zanr;


            var query = new CypherQuery("match (n:Zanr) where n.naziv='"+imeZanra+"' return n",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            zanr = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Zanr>(query).FirstOrDefault();

            return zanr;
        }

        public Zanr VratiZanrBenda(string imeBenda)
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            Zanr zanr;


            var query = new CypherQuery("match (n:Zanr)-[r:VRSTA_MUZIKE]->(m:Bend) where m.naziv='" + imeBenda + "' return n",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            zanr = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Zanr>(query).FirstOrDefault();

            return zanr;
        }
    }
}
