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
    public class DrzavaServis
    {
        public static string drzavainstancaIme;
        public DrzavaServis(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        public List<Drzava> VratiSveDrzavePoKontinentu(string imekontinenta)
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            List<Drzava> Drzave;


            var query = new CypherQuery("match (n:Kontinent)-[r:SASTOJI_SE_OD]->(m:Drzava) where n.ime='" + imekontinenta + "' return m",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            Drzave = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Drzava>(query).ToList();

            return Drzave;
        }
        public List<Drzava> VratiSveDrzave()
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            List<Drzava> Drzave;


            var query = new CypherQuery("match (n:Drzava) return n",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            Drzave = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Drzava>(query).ToList();

            return Drzave;
        }

        public Drzava VratiDrzavaPoIme(string imedrzave)
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            Drzava drzava;


            var query = new CypherQuery("match (n:Drzava) where n.ime='" + imedrzave + "' return n",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            drzava = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Drzava>(query).FirstOrDefault();

            return drzava;
        }

        public Drzava VratiDrzavuBenda(string imeBenda)
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            Drzava drzava;



            var query = new CypherQuery("match (n:Bend)-[r:OSNOVAN_U]->(m:Drzava) where n.naziv='" + imeBenda + "' return m",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            drzava = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Drzava>(query).FirstOrDefault();

            return drzava;
        }
    }
}
