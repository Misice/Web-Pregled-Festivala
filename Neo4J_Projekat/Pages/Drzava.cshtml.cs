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
    public class DrzavaModel : PageModel
    {
        public DrzavaServis Drzavaservis;
        public GradServis Gradservis;
        public Drzava drzava { get; private set; }
        public List<Grad> gradovi { get; private set; }

        public DrzavaModel(DrzavaServis drzavaservis, GradServis gradservis)
        {
            Drzavaservis = drzavaservis;
            Gradservis = gradservis;
        }
        public void OnGet()
        {
            drzava = Drzavaservis.VratiDrzavaPoIme(DrzavaServis.drzavainstancaIme);
            gradovi = Gradservis.VratiSveGradovePoDrzavi(drzava.ime);
        }

        public IActionResult OnPostGrad(Grad grad)
        {
            GradServis.gradinstancaIme = grad.ime;

            return RedirectToPage("/Grad");
        }
    }
}
