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
    public class BendServis
    {
        public static string bendinstancaIme;
        public BendServis(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        public List<Bend> VratiSveBendove()
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            List<Bend> Bendovi;


            var query = new CypherQuery("match (n:Bend) return n",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            Bendovi = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Bend>(query).ToList();

            return Bendovi;
        }
        public List<Bend> VratiSveBendovePoZanru(string imeZanra)
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            List<Bend> Bendovi;


            var query = new CypherQuery("match (n:Zanr)-[r:VRSTA_MUZIKE]->(m:Bend) where n.naziv='" + imeZanra + "' return m",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            Bendovi = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Bend>(query).ToList();

            return Bendovi;
        }

        public List<Bend> VratiSveBendovePoGodiniSviranjaNaFestivalu(string imeFestivala, string godina)
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            List<Bend> Bendovi;


            var query = new CypherQuery("match (n:Festival)-[r:ODRZAVA_SE_" + godina + "]->(b)<-[t:SVIRAJU_" + godina + "]-(m:Bend) where n.naziv='" + imeFestivala + "' return m",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            Bendovi = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Bend>(query).ToList();

            return Bendovi;
        }

        public List<string> VratiSveGodineSviranjaBenda(string imeBenda)
        {
            GraphClient sesija = SessionMenager.GetSession();
            sesija.Connect();

            List<string> godine;

            var query = new CypherQuery("match (n:Bend)-[r]->(m) where type(r) CONTAINS 'SVIRAJU_' and n.naziv='" + imeBenda + "' return DISTINCT  type(r)",
                                                          new Dictionary<string, object>(), CypherResultMode.Set);

            godine = ((IRawGraphClient)sesija).ExecuteGetCypherResults<string>(query).ToList();

            List<string> pomGodine = new List<string>();

            foreach (var godina in godine)
            {
                string[] pom = godina.Split('_');
                pomGodine.Add(pom.Last());
            }


            return pomGodine;
        }
        public Bend VratiBendPoIme(string imebenda)
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            Bend bend;


            var query = new CypherQuery("match (n:Bend) where n.naziv='" + imebenda + "' return n",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            bend = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Bend>(query).FirstOrDefault();

            return bend;
        }

        public List<Muzicar> VratiSveMuzicareuBendu(string imeBenda)
        {

            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            List<Muzicar> Muzicari;


            var query = new CypherQuery("match (n:Bend)-[r:SVIRA]->(m:Muzicar) where n.naziv='" + imeBenda + "' return m",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            Muzicari = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Muzicar>(query).ToList();

            return Muzicari;
        }

        public Muzicar VratiMuzicaraIzBendaPoIme(string imeBenda, string imeMuzicara)
        {
            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


            Muzicar muzicar;


            var query = new CypherQuery("match (n:Bend)-[r:SVIRA]->(m:Muzicar) where n.naziv='" + imeBenda + "' AND m.ime='"+ imeMuzicara + "' return m",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);


            muzicar = ((IRawGraphClient)sesija).ExecuteGetCypherResults<Muzicar>(query).FirstOrDefault();

            return muzicar;

        }

        public void DodajMuzicaraUBend(string imeBenda,string imeMuzicara)
        {
            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();


           var query1= new CypherQuery("CREATE (n:Muzicar { ime: '"+imeMuzicara+"'})",
                                                           new Dictionary<string, object>(), CypherResultMode.Set);

           var query2= new CypherQuery("MATCH(a:Bend),(b: Muzicar) WHERE a.naziv = '"+imeBenda+"' AND b.ime = '"+ imeMuzicara + "' CREATE (a)-[r:SVIRA]->(b) return a",
                                                         new Dictionary<string, object>(), CypherResultMode.Set);

            ((IRawGraphClient)sesija).ExecuteCypher(query1);

            ((IRawGraphClient)sesija).ExecuteCypher(query2);

        }
        public void ObrisiMuzicaraIzBend(string imeBenda, string imeMuzicara)
        {
            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();
        
            var query = new CypherQuery("MATCH(a:Bend),(b: Muzicar) WHERE a.naziv = '" + imeBenda + "' AND b.ime = '" + imeMuzicara + "' DETACH DELETE b",
                                                          new Dictionary<string, object>(), CypherResultMode.Set);

            ((IRawGraphClient)sesija).ExecuteCypher(query);
        }
        public void IzmeniImeMuzicaraIzBend(string ImeBenda, string ImeMuzicara,string NovoImeMuzicara)
        {
            GraphClient sesija = SessionMenager.GetSession();

            sesija.Connect();

            var query = new CypherQuery("MATCH(a:Bend),(b: Muzicar) WHERE a.naziv = '" + ImeBenda + "' AND b.ime = '" + ImeMuzicara + "' SET b.ime='"+ NovoImeMuzicara + "' ",
                                                          new Dictionary<string, object>(), CypherResultMode.Set);

            ((IRawGraphClient)sesija).ExecuteCypher(query);
        }

    }
}
