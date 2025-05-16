using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class ListeProductsModel : PageModel
    {
        [BindProperty]
        public RequestData Request { get; set; } = new RequestData();
        public bool HasData { get; set; } = false;
        public Invoice Invoice { get; set; } = new Invoice();

        public void OnGet()
        {
            HasData = false;
        }

        public IActionResult OnPost()
        {
            // Vérifier si les données du formulaire sont valides
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Validation des valeurs
            if (Request.Length <= 0 || Request.Width <= 0)
            {
                ModelState.AddModelError("", "Les valeurs de longueur et largeur doivent être positives.");
                return Page();
            }

            if (string.IsNullOrEmpty(Request.FloorType))
            {
                ModelState.AddModelError("", "Veuillez sélectionner un type de couvre-plancher.");
                return Page();
            }

            CalculateInvoice();
            HasData = true;
            Console.WriteLine($"Devis généré : Total = {Invoice.Total} $");
            return Page();
        }

        private void CalculateInvoice()
        {
            Invoice.Area = Request.Length * Request.Width;
            (Invoice.MaterialCost, Invoice.LaborCost) = GetCosts(Request.FloorType);
            Invoice.Total = (Invoice.MaterialCost + Invoice.LaborCost) * 1.15; // Taxes 15%
        }

        private (double, double) GetCosts(string floorType)
        {
            return floorType switch
            {
                "tapis-commercial" => (1.29 * Invoice.Area, 2.00 * Invoice.Area),
                "tapis-qualite" => (3.99 * Invoice.Area, 2.25 * Invoice.Area),
                "plancher-bois-franc" => (3.49 * Invoice.Area, 3.25 * Invoice.Area),
                "plancher-flottant" => (1.99 * Invoice.Area, 2.25 * Invoice.Area),
                "ceramique" => (1.49 * Invoice.Area, 3.25 * Invoice.Area),
                _ => (0, 0)
            };
        }
    }

    public class RequestData
    {
        public double Length { get; set; }
        public double Width { get; set; }
        public string FloorType { get; set; }
    }

    public class Invoice
    {
        public double Area { get; set; }
        public double MaterialCost { get; set; }
        public double LaborCost { get; set; }
        public double Total { get; set; }
    }
}