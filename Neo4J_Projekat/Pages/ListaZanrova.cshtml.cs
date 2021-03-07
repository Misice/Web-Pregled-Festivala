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
    public class ListaZanrovaModel : PageModel
    {
        public ZanrServis Zanrservis;
        public List<Zanr> zanrovi { get; private set; }
        public ListaZanrovaModel(ZanrServis zanrservis)
        {
            Zanrservis = zanrservis;
        }

        public void OnGet()
        {
            zanrovi = Zanrservis.VratiSveZanrove();
        }

        public IActionResult OnPostZanr(Zanr zanr)
        {
            ZanrServis.zanrinstancaIme = zanr.naziv;

            return RedirectToPage("/Zanr");
        }
    }
}
