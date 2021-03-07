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
    public class GradModel : PageModel
    {
        public GradServis Gradservis;
        public FestivalServis Festivalservis;
        public Grad grad { get; private set; }
        public List<Festival> festivali { get; private set; }
        public GradModel(GradServis gradservis, FestivalServis festivalservis)
        {
            Gradservis = gradservis;
            Festivalservis = festivalservis;
        }


        public void OnGet()
        {
            grad = Gradservis.VratiGradPoIme(GradServis.gradinstancaIme);


            festivali = Festivalservis.VratiSveFestivalePoGradu(grad.ime);
        }
        public IActionResult OnPostFestival(Festival festival)
        {
            FestivalServis.festivalinstancaIme = festival.naziv;

            return RedirectToPage("/Festival");
        }

    }
}
