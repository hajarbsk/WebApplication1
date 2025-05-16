using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using System.Xml.Linq;

namespace WebApplication1.Pages
{
    public class formModel : PageModel
    {
        // Propri�t�s publiques accessibles depuis la vue
        public bool HasData { get; set; } = false;
        public double Area { get; set; }
        public double MaterialCost { get; set; }
        public double InstallationCost { get; set; }
        public double TotalCost { get; set; }
        public string FloorType { get; set; } = string.Empty;



        // M�thode ex�cut�e lors du chargement initial de la page (GET)
        public void OnGet()
        {
            HasData = false; // R�initialisation
        }

        public bool hasData = false; 
        public string data = "";

        // M�thode ex�cut�e lors de la soumission du formulaire (POST)
        public void OnPost()
        {
            // V�rifie si les champs n�cessaires existent dans le formulaire
            if (Request.Form["length"].Count > 0 && Request.Form["width"].Count > 0 && Request.Form["type"].Count > 0)
            {
                // R�cup�ration des donn�es du formulaire
                double length = double.Parse(Request.Form["length"]);
                double width = double.Parse(Request.Form["width"]);
                FloorType = Request.Form["type"];

                // Calculs pour Plancher Expert
                Area = length * width;
                MaterialCost = FloorType == "carrelage" ? 20 * Area : 30 * Area; // Exemple de co�t par m�
                InstallationCost = FloorType == "carrelage" ? 10 * Area : 15 * Area; // Exemple de co�t par m�
                TotalCost = (MaterialCost + InstallationCost) * 1.15; // Taxes 15%

                // Marquer que les donn�es ont �t� soumises
                HasData = true;

                // Afficher les donn�es dans la console pour v�rification
                Console.WriteLine($"Superficie: {Area} m�, Type: {FloorType}, Co�t total: {TotalCost} �");
            }
        }
    }
}
