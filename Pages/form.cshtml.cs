using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using System.Xml.Linq;

namespace WebApplication1.Pages
{
    public class formModel : PageModel
    {
        // Propriétés publiques accessibles depuis la vue
        public bool HasData { get; set; } = false;
        public double Area { get; set; }
        public double MaterialCost { get; set; }
        public double InstallationCost { get; set; }
        public double TotalCost { get; set; }
        public string FloorType { get; set; } = string.Empty;



        // Méthode exécutée lors du chargement initial de la page (GET)
        public void OnGet()
        {
            HasData = false; // Réinitialisation
        }

        public bool hasData = false; 
        public string data = "";

        // Méthode exécutée lors de la soumission du formulaire (POST)
        public void OnPost()
        {
            // Vérifie si les champs nécessaires existent dans le formulaire
            if (Request.Form["length"].Count > 0 && Request.Form["width"].Count > 0 && Request.Form["type"].Count > 0)
            {
                // Récupération des données du formulaire
                double length = double.Parse(Request.Form["length"]);
                double width = double.Parse(Request.Form["width"]);
                FloorType = Request.Form["type"];

                // Calculs pour Plancher Expert
                Area = length * width;
                MaterialCost = FloorType == "carrelage" ? 20 * Area : 30 * Area; // Exemple de coût par m²
                InstallationCost = FloorType == "carrelage" ? 10 * Area : 15 * Area; // Exemple de coût par m²
                TotalCost = (MaterialCost + InstallationCost) * 1.15; // Taxes 15%

                // Marquer que les données ont été soumises
                HasData = true;

                // Afficher les données dans la console pour vérification
                Console.WriteLine($"Superficie: {Area} m², Type: {FloorType}, Coût total: {TotalCost} €");
            }
        }
    }
}
