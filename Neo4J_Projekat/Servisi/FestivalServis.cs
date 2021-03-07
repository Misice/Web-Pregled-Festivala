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
    public class FestivalServis
    {
        public static string festivalinstancaIme;
        public FestivalServis(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        public List<Festival> VratiSveFestivale()
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            List<Festival> Festivali;

            var query = new CypherQuery("match (n:Festival) return n",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            Festivali = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Festival>(query).ToList();

            return Festivali;
        }
        public List<Festival> VratiSveFestivaleGdeSviraBendPoGodini(string imeBenda, string godina)
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            List<Festival> festivali;


            var query = new CypherQuery("match (n:Bend)-[r:SVIRAJU_" + godina + "]->(b)<-[t:ODRZAVA_SE_" + godina + "]-(m:Festival) where n.naziv='" + imeBenda + "' return m",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            festivali = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Festival>(query).ToList();

            return festivali;
        }
        public List<string> VratiSveGodineOdrzavanjaFestivala(string imeFestivala)
        {
            GraphClient sesija = SessionMenager.GetSession();
            sesija.Connect();

            List<string> godine;

            var query = new CypherQuery("match (n:Festival)-[r]->(m) where type(r) CONTAINS 'ODRZAVA_SE_' and n.naziv='"+ imeFestivala + "' return type(r)",
                                                          new Dictionary<string, object>(), CypherResultMode.Set);

            godine = ((IRawGraphClient)sesija).ExecuteGetCypherResults<string>(query).ToList();

            List<string> pomGodine=new List<string>();

            foreach (var godina in godine)
            {
                string[] pom = godina.Split('_');
                pomGodine.Add(pom.Last()) ;
            }
          
            return pomGodine;
        }

        public Festival VratiFestivalPoIme(string imeFestivala)
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            Festival festival;


            var query = new CypherQuery("match (n:Festival) where n.naziv='" + imeFestivala + "' return n",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            festival = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Festival>(query).FirstOrDefault();

            return festival;
        }

        public List<Festival> VratiSveFestivalePoGradu(string imegrada)
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            List<Festival> festivali;


            var query = new CypherQuery("match (n:Grad)-[r:ODRZAVA_SE]->(m:Festival) where n.ime='" + imegrada + "' return m",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            festivali = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Festival>(query).ToList();

            return festivali;
        }
    }
}
