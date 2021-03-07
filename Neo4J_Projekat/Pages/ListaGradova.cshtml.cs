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
    public class ListaGradovaModel : PageModel
    {
        public GradServis Gradservis;
        public Grad OdabraniGrad { get; private set; }
        public List<Grad> Gradovi { get; private set; }
        public ListaGradovaModel(GradServis gradservis)
        {
            Gradservis = gradservis;
        }

        public void OnGet()
        {
            Gradovi = Gradservis.VratiSveGradove();
        }
        public IActionResult OnPostGrad(Grad grad)
        {
            GradServis.gradinstancaIme= grad.ime;

            return RedirectToPage("/Grad");
        }

        public void OnPostVratiSve()
        {
            Gradovi = Gradservis.VratiSveGradove();
        }

        public void OnPostPronadjiJedan(string ime)
        {
            OdabraniGrad = Gradservis.VratiGradPoIme(ime);

            if (OdabraniGrad != null)
            {
                Gradovi = new List<Grad>();
                Gradovi.Add(OdabraniGrad);
            }
        }

    }
}
