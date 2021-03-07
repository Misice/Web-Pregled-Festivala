using Microsoft.AspNetCore.Http;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neo4J_Projekat.Modeli
{
    public class SessionMenager
    {
        public static GraphClient klijent;

        public static GraphClient GetSession()
        {
            if (klijent == null)
            {
                   klijent = new GraphClient(new Uri("http://localhost:7474/db/data/"),"neo4j","admin");
            }

            return klijent;
        }
    }
}
