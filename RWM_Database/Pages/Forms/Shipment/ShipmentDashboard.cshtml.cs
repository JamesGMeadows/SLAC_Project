using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RWM_Database.Backend;
using RWM_Database.Utility;

namespace RWM_Database.Pages.Forms.Shipment
{

    /* 
    * Class description: Shows a list of shipments in a table format
    * 
    * Author: James Meadows
    * Intern at SLAC during summer of 2021
    * 
    */

    public class ShipmentDashboardModel : PageModel
    {

        public ShipmentHandler ShipmentHandler { get; set; }

        //Input in the search field of the dashboard
        [BindProperty(Name = "SearchShipmentNumber", SupportsGet = true)]
        public string SearchShipmentNumber { get; set; }

        //Input in the search field of the dashboard
        [BindProperty(Name = "SearchShipmentType", SupportsGet = true)]
        public string SearchShipmentType { get; set; }

        //For table pagination
        [BindProperty(Name = "CurrentPage", SupportsGet = true)]
        public int CurrentPage { get; set; }

        public PaginatedTable PaginatedTable { get; set; }

        public void OnGet()
        {

            ShipmentHandler = new ShipmentHandler();
            SearchByField search = ShipmentHandler.Search;

            search.AddSearch("shipment_number",SearchShipmentNumber);
            search.AddSearch("type_name", SearchShipmentType);

            ShipmentHandler.GetShipmentList();

            PaginatedTable = new PaginatedTable(10, ShipmentHandler.ShipmentList.Count);


        }

        public IActionResult OnPostSearchButton(IFormCollection data)
        {

            return RedirectToPage("ShipmentDashboard", new { SearchShipmentNumber = data["SearchShipmentNumber"], SearchShipmentType = data["SearchShipmentType"] });
        }

        public IActionResult OnPostClearButton(IFormCollection data)
        {
            return RedirectToPage("ShipmentDashboard");
        }

        public IActionResult OnPostCreateButton()
        {

            return RedirectToPage("CreateShipment");
        }

    }
}
