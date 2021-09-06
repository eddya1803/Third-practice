using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Third_practice.Pages
{
    public class PrestamoModel : PageModel
    {
        public double monto { get; set; }
        public double cantidadCuotas { get; set; }
        public double porcentajeInteresAnual { get; set; }
        public double cuota { get; set; }

        public PrestamoModel()
        {
            this.cuota = 0;
        }

        public void OnGet(double monto, double cantidadCuotas, double porcentajeInteresAnual)
        {
            this.monto = monto;
            this.cantidadCuotas = cantidadCuotas;
            this.porcentajeInteresAnual = porcentajeInteresAnual / 100;
            double porcentajeInteresMensual = this.porcentajeInteresAnual / 12;
            this.cuota = (this.monto * porcentajeInteresMensual) / (1 - Math.Pow(1 + porcentajeInteresMensual, -this.cantidadCuotas));
            this.cuota = Math.Round(this.cuota, 2);
        }

    }
}
