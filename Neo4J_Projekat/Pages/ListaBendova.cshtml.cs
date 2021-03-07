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
    public class ListaBendovaModel : PageModel
    {
        public BendServis Bendservis;
        public Bend OdabraniBend { get; private set; }
        public List<Bend> Bendovi { get; private set; }
        public ListaBendovaModel(BendServis bendservis)
        {
            Bendservis = bendservis;
        }

        public void OnGet()
        {
            Bendovi = Bendservis.VratiSveBendove();
        }
        public IActionResult OnPostBend(Bend bend)
        {
           BendServis.bendinstancaIme = bend.naziv;

            return RedirectToPage("/Bend");
        }
        
        public void OnPostVratiSve()
        {
            Bendovi = Bendservis.VratiSveBendove();
        }

        public void OnPostPronadjiJedan(string naziv)
        {
            OdabraniBend = Bendservis.VratiBendPoIme(naziv);

            if (OdabraniBend != null)
            {
                Bendovi = new List<Bend>();
                Bendovi.Add(OdabraniBend);
            }
        }
    }
}
