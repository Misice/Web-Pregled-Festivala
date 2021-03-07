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
    public class KontinentModel : PageModel
    {

        public DrzavaServis Drzavaservis;
        public KontinentServis Kontinentservis;
        public Kontinent kontinent { get; private set; }
        public List<Drzava> drzave { get; private set; }

        public KontinentModel(DrzavaServis drzavaservis, KontinentServis kontinentservis)
        {
            Drzavaservis = drzavaservis;
            Kontinentservis = kontinentservis;
        }
        public void OnGet()
        {
            kontinent = Kontinentservis.VratiKontinentPoIme(KontinentServis.kontinentinstancaIme);
            drzave = Drzavaservis.VratiSveDrzavePoKontinentu(kontinent.ime);
        }
        public IActionResult OnPostDrzava(Drzava drzava)
        {
            DrzavaServis.drzavainstancaIme = drzava.ime;

            return RedirectToPage("/Drzava");
        }
    }
}
