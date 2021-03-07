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
    public class BendModel : PageModel
    {
        public BendServis Bendservis;
        public ZanrServis Zanrservis;
        public DrzavaServis Drzavaservis;
        public FestivalServis Festivalservis;
        public Bend bend { get; private set; }
        public string OdabranaGodinaSviranja { get; private set; }
        public Drzava DrzavaOsnivanja { get; private set; }
        public Zanr ZanrBenda { get; private set; }
        public List<Muzicar> Muzicari { get; private set; }
        public List<Festival> Festivali { get; private set; }
        public List<string> GodineOdrzavanja { get; private set; }

        public BendModel(BendServis bendservis, ZanrServis zanrservis,DrzavaServis drzavaservis, FestivalServis festivalservis)
        {
            Bendservis = bendservis;
            Zanrservis = zanrservis;
            Drzavaservis = drzavaservis;
            Festivalservis = festivalservis;
        }
        public void OnGet()
        {
            bend = Bendservis.VratiBendPoIme(BendServis.bendinstancaIme);
            Muzicari = Bendservis.VratiSveMuzicareuBendu(bend.naziv);
            ZanrBenda = Zanrservis.VratiZanrBenda(bend.naziv);
            DrzavaOsnivanja = Drzavaservis.VratiDrzavuBenda(bend.naziv);
            GodineOdrzavanja = Bendservis.VratiSveGodineSviranjaBenda(bend.naziv);
        }

        public void OnPostGodina(string godina)
        {
            bend = Bendservis.VratiBendPoIme(BendServis.bendinstancaIme);
            Muzicari = Bendservis.VratiSveMuzicareuBendu(bend.naziv);
            ZanrBenda = Zanrservis.VratiZanrBenda(bend.naziv);
            DrzavaOsnivanja = Drzavaservis.VratiDrzavuBenda(bend.naziv);
            GodineOdrzavanja = Bendservis.VratiSveGodineSviranjaBenda(bend.naziv);
            Festivali = Festivalservis.VratiSveFestivaleGdeSviraBendPoGodini(bend.naziv,godina);
            OdabranaGodinaSviranja = godina;
        }
        public IActionResult OnPostFestival(Festival festival)
        {
            FestivalServis.festivalinstancaIme = festival.naziv;

            return RedirectToPage("/Festival");
        }
        public IActionResult OnPostZanr(Zanr zanr)
        {
            ZanrServis.zanrinstancaIme = zanr.naziv;

            return RedirectToPage("/Zanr");
        }
        public IActionResult OnPostDrzava(Drzava drzava)
        {
            DrzavaServis.drzavainstancaIme = drzava.ime;

            return RedirectToPage("/Drzava");
        }

    }
}
