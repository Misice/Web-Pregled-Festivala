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
    public class ListaDrzavaModel : PageModel
    {
        public DrzavaServis Drzavaservis;
        public Drzava OdabranaDrzava { get; private set; }
        public List<Drzava> Drzave { get; private set; }
        public ListaDrzavaModel(DrzavaServis drzavaservis)
        {
            Drzavaservis = drzavaservis;
        }

        public void OnGet()
        {
            Drzave = Drzavaservis.VratiSveDrzave();
        }

        public IActionResult OnPostDrzava(Drzava drzava)
        {
            DrzavaServis.drzavainstancaIme = drzava.ime;

            return RedirectToPage("/Drzava");
        }

        public void OnPostVratiSve()
        {
            Drzave = Drzavaservis.VratiSveDrzave();
        }

        public void OnPostPronadjiJedan(string ime)
        {
            OdabranaDrzava = Drzavaservis.VratiDrzavaPoIme(ime);

            if (OdabranaDrzava != null)
            {
                Drzave = new List<Drzava>();
                Drzave.Add(OdabranaDrzava);
            }
        }

    }
}
