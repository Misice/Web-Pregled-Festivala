using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Neo4J_Projekat.Modeli;
using Neo4J_Projekat.Servisi;

namespace Neo4J_Projekat.Pages
{
    public class ZanrModel : PageModel
    {

        public BendServis Bendservis;
        public ZanrServis Zanrservis;
        public Zanr zanr { get; private set; }
        public List<Bend> Bendovi { get; private set; }
        public ZanrModel(BendServis bendservis, ZanrServis zanrservis)
        {
            Bendservis = bendservis;
            Zanrservis = zanrservis;
        }


        public void OnGet()
        {
            zanr = Zanrservis.VratiZanrPoIme(ZanrServis.zanrinstancaIme);  
            Bendovi = Bendservis.VratiSveBendovePoZanru(zanr.naziv);
        }

        public IActionResult OnPostBend(Bend bend)
        {
            BendServis.bendinstancaIme = bend.naziv;

            return RedirectToPage("/Bend");
        }
    }
}
