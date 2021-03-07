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
    public class ListaFestivalaModel : PageModel
    {
        public FestivalServis Festivalservis;
        public Festival OdabraniFestival { get; private set; }
        public List<Festival> Festivali { get; private set; }
        public ListaFestivalaModel(FestivalServis festivalservis)
        {
            Festivalservis = festivalservis;
        }

        public void OnGet()
        {
            Festivali = Festivalservis.VratiSveFestivale();
        }
        public IActionResult OnPostFestival(Festival festival)
        {
            FestivalServis.festivalinstancaIme = festival.naziv;

            return RedirectToPage("/Festival");
        }
        public void OnPostVratiSve()
        {
            Festivali = Festivalservis.VratiSveFestivale();
        }

        public void OnPostPronadjiJedan(string naziv)
        {
            OdabraniFestival = Festivalservis.VratiFestivalPoIme(naziv);

            if (OdabraniFestival != null)
            {
                Festivali = new List<Festival>();
                Festivali.Add(OdabraniFestival);
            }
        }

    }
}
