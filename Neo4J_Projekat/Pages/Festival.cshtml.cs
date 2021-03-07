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
    public class FestivalModel : PageModel
    {
        public FestivalServis Festivalservis;
        public Festival festival { get; private set; }

        public BendServis Bendservis;
        public string TrenutnaGodinaOdrzavanja { get; private set; }
        public List<Bend> Bendovi { get; private set; }
        public List<string> GodineOdrzavanja { get; private set; }
        public FestivalModel(FestivalServis festivalservis,BendServis bendservis)
        {
            Festivalservis = festivalservis;
            Bendservis = bendservis;
        }

        public void OnGet()
        {
            festival = Festivalservis.VratiFestivalPoIme(FestivalServis.festivalinstancaIme);
            GodineOdrzavanja = Festivalservis.VratiSveGodineOdrzavanjaFestivala(festival.naziv);
        }
        public IActionResult OnPostBend(Bend bend)
        {
            BendServis.bendinstancaIme = bend.naziv;

            return RedirectToPage("/Bend");
        }
        public void OnPostGodina(string godina)
        {
            festival = Festivalservis.VratiFestivalPoIme(FestivalServis.festivalinstancaIme);
            GodineOdrzavanja = Festivalservis.VratiSveGodineOdrzavanjaFestivala(festival.naziv);
            TrenutnaGodinaOdrzavanja = godina;
            Bendovi = Bendservis.VratiSveBendovePoGodiniSviranjaNaFestivalu(festival.naziv, godina);
        }
    }
}
