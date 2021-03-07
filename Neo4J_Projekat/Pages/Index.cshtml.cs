using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Neo4J_Projekat.Modeli;
using Neo4J_Projekat.Servisi;

namespace Neo4J_Projekat.Pages
{
    public class IndexModel : PageModel
    {
        public KontinentServis KontinentServis;
        public List<Kontinent> Kontinenti { get; private set; }
        public IndexModel(KontinentServis kontinentservis)
        {
            KontinentServis = kontinentservis;
        }

        public void OnGet()
        {
            Kontinenti = KontinentServis.VratiSveKontinente();
        }
        public IActionResult OnPostKontinent(Kontinent kontinent)
        {
           KontinentServis.kontinentinstancaIme = kontinent.ime;

            return RedirectToPage("/Kontinent");
        }
    }
}
