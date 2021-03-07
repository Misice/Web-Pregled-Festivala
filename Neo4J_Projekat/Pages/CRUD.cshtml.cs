using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Neo4J_Projekat.Servisi;
using Neo4J_Projekat.Modeli;

namespace Neo4J_Projekat.Pages
{
    public class CRUDModel : PageModel
    {
        public BendServis Bendservis { get; private set; }
        public bool sakrivenoDodavanje = false;
        public bool sakrivenoDodavanjeGreska = false;

        public bool sakrivenoBrisanje = false;
        public bool sakrivenoBrisanjeGreska = false;

        public bool sakrivenoMenjanje = false;
        public bool sakrivenoMenjanjeGreska = false;
        public CRUDModel(BendServis bendservis)
        {

            Bendservis = bendservis;
        }
        public void OnGet()
        {
            
        }

        public void OnPostSubmitDodavanje(string NazivBenda, string NazivMuzicara)
        {
            Bend pombend = Bendservis.VratiBendPoIme(NazivBenda);

            if (pombend == null)
                this.sakrivenoDodavanjeGreska = !this.sakrivenoDodavanjeGreska;
            else if (!sakrivenoDodavanje)
                this.sakrivenoDodavanje = !sakrivenoDodavanje;

            if (pombend != null)
                Bendservis.DodajMuzicaraUBend(NazivBenda, NazivMuzicara);
        }

        public void OnPostSubmitBrisanje(string NazivBenda, string NazivMuzicara)
        {     
            Bend pombend = Bendservis.VratiBendPoIme(NazivBenda);
            Muzicar muzicarpom = Bendservis.VratiMuzicaraIzBendaPoIme(NazivBenda, NazivMuzicara);

            if ((pombend == null) || (muzicarpom == null))
                this.sakrivenoBrisanjeGreska = !this.sakrivenoBrisanjeGreska;
            else if (!sakrivenoBrisanje)
                this.sakrivenoBrisanje = !sakrivenoBrisanje;

            if ((pombend != null) && (muzicarpom != null))
                Bendservis.ObrisiMuzicaraIzBend(NazivBenda, NazivMuzicara);

        }
        public void OnPostSubmitIzmena(string NazivBenda, string NazivMuzicara,string NoviNazivMuzicara)
        {
             Bend pombend = Bendservis.VratiBendPoIme(NazivBenda);
            Muzicar muzicarpom = Bendservis.VratiMuzicaraIzBendaPoIme(NazivBenda, NazivMuzicara);

            if (pombend == null || muzicarpom == null)
                this.sakrivenoMenjanjeGreska = !this.sakrivenoMenjanjeGreska;
            else if (!sakrivenoMenjanje)
                this.sakrivenoMenjanje = !sakrivenoMenjanje;
            

            if ((pombend != null) && (muzicarpom != null))
               Bendservis.IzmeniImeMuzicaraIzBend(NazivBenda, NazivMuzicara, NoviNazivMuzicara);
        }
    }
}
