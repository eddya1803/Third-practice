using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Third_practice.Pages
{
    public class CajeroModel : PageModel
    {
        public string banco { get; set; }
        public double montoSolicitado { get; set; }
        public int cantidadBilletesDeMil { get; set; }
        public int cantidadBilletesDeQuinientos { get; set; }
        public int cantidadBilletesDeCien { get; set; }
        public double montoDisponible { get; set; }
        public double limiteDeRetiro { get; set; }
        public double montoDispensado { get; set; }
        public string mensaje { get; set; }

        public CajeroModel()
        {
            this.montoDispensado = 0;
        }

        public void OnGet(double montoSolicitado, string banco = "")
        {
            this.banco = banco;
            this.montoSolicitado = montoSolicitado;
            this.cantidadBilletesDeMil = 9;
            this.cantidadBilletesDeQuinientos = 19;
            this.cantidadBilletesDeCien = 99;
            this.montoDisponible = (this.cantidadBilletesDeMil * 1000) + (this.cantidadBilletesDeQuinientos * 500) + (this.cantidadBilletesDeCien * 100);

            //Comprobando cual banco ha sido seleccionado
            if (this.banco == "Banco ABC")
            {
                this.limiteDeRetiro = 10000;

                //Comprobando si el monto solicitado no excede el limite de retiro
                if (this.montoSolicitado > this.limiteDeRetiro)
                {
                    this.mensaje = "El monto solicitado excede el límite de retiro por transacción.";
                }

                if ((this.montoSolicitado > 1000) && (this.EstanTodosLosBilletes()))
                {
                    int cantidadBilletesMil = 0;
                    int cantidadBilletesQuinientos = 0;
                    float cantidadBilletesCien = 0;
                    this.montoDispensado = this.montoSolicitado;

                    if (this.montoSolicitado >= 1000)
                    {
                        this.montoSolicitado = this.montoSolicitado / 1000;
                        cantidadBilletesMil = (int)Math.Truncate(this.montoSolicitado);
                    }
                    else
                    {
                        cantidadBilletesMil = 0;
                    }

                    if ((this.montoSolicitado - cantidadBilletesMil) >= 0.5)
                    {
                        cantidadBilletesQuinientos = 1;
                    }
                    else
                    {
                        cantidadBilletesQuinientos = 0;
                    }

                    if (cantidadBilletesQuinientos == 1)
                    {
                        cantidadBilletesCien = (float) (Math.Round(this.montoSolicitado - cantidadBilletesMil, 1) - 0.5) * 10;
                    }
                    else
                    {
                        cantidadBilletesCien = (float) Math.Round(this.montoSolicitado - cantidadBilletesMil, 1) * 10;
                    }

                    this.montoDisponible = this.montoDisponible - montoDispensado;
                    this.cantidadBilletesDeMil = this.cantidadBilletesDeMil - cantidadBilletesMil;
                    this.cantidadBilletesDeQuinientos = this.cantidadBilletesDeQuinientos - cantidadBilletesQuinientos;
                    this.cantidadBilletesDeCien = this.cantidadBilletesDeCien - (int)cantidadBilletesCien;

                    this.mensaje = "El monto dispensado es: " + montoDispensado + ", la cantidad de billetes de 1000 dispensados es: "
                        + cantidadBilletesMil + ", la cantidad de billetes de 500 es: " + cantidadBilletesQuinientos
                        + ", la cantidad de billetes de 100 dispensados es: " + cantidadBilletesCien;

                }
                else if ((this.montoSolicitado < 1000) && (this.EstanTodosLosBilletes()))
                {
                    int cantidadBilletesMil = 0;
                    int cantidadBilletesQuinientos = 0;
                    float cantidadBilletesCien = 0;
                    double montoDispensado = this.montoSolicitado;

                    if (this.montoSolicitado >= 500)
                    {
                        cantidadBilletesQuinientos = 1;
                    }
                    else
                    {
                        cantidadBilletesQuinientos = 0;
                    }

                    if (this.montoSolicitado < 500)
                    {
                        cantidadBilletesCien = (float)Math.Round(this.montoSolicitado / 100, 1);
                    }
                    else
                    {
                        cantidadBilletesCien = (float)Math.Round(this.montoSolicitado - 500, 1) / 100;
                    }

                    this.montoDisponible = this.montoDisponible - montoDispensado;
                    this.cantidadBilletesDeMil = this.cantidadBilletesDeMil - cantidadBilletesMil;
                    this.cantidadBilletesDeQuinientos = this.cantidadBilletesDeQuinientos - cantidadBilletesQuinientos;
                    this.cantidadBilletesDeCien = this.cantidadBilletesDeCien - (int)cantidadBilletesCien;

                    this.mensaje = "El monto dispensado es: " + montoDispensado + ", la cantidad de billetes de 1000 dispensados es: "
                        + cantidadBilletesMil + ", la cantidad de billetes de 500 es: " + cantidadBilletesQuinientos
                        + ", la cantidad de billetes de 100 dispensados es: " + cantidadBilletesCien;
                }
            }
            else
            {
                this.limiteDeRetiro = 2000;

                //Comprobando si el monto solicitado no excede el limite de retiro
                if (this.montoSolicitado > this.limiteDeRetiro)
                {
                    this.mensaje = "El monto solicitado excede el límite de retiro por transacción.";
                }
                else
                {
                    if ((this.montoSolicitado > 1000) && (this.EstanTodosLosBilletes()))
                    {
                        int cantidadBilletesMil = 0;
                        int cantidadBilletesQuinientos = 0;
                        float cantidadBilletesCien = 0;
                        this.montoDispensado = this.montoSolicitado;

                        if (this.montoSolicitado >= 1000)
                        {
                            this.montoSolicitado = this.montoSolicitado / 1000;
                            cantidadBilletesMil = (int)Math.Truncate(this.montoSolicitado);
                        }
                        else
                        {
                            cantidadBilletesMil = 0;
                        }

                        if ((this.montoSolicitado - cantidadBilletesMil) >= 0.5)
                        {
                            cantidadBilletesQuinientos = 1;
                        }
                        else
                        {
                            cantidadBilletesQuinientos = 0;
                        }

                        if (cantidadBilletesQuinientos == 1)
                        {
                            cantidadBilletesCien = (float)(Math.Round(this.montoSolicitado - cantidadBilletesMil, 1) - 0.5) * 10;
                        }
                        else
                        {
                            cantidadBilletesCien = (float)Math.Round(this.montoSolicitado - cantidadBilletesMil, 1) * 10;
                        }

                        this.montoDisponible = this.montoDisponible - montoDispensado;
                        this.cantidadBilletesDeMil = this.cantidadBilletesDeMil - cantidadBilletesMil;
                        this.cantidadBilletesDeQuinientos = this.cantidadBilletesDeQuinientos - cantidadBilletesQuinientos;
                        this.cantidadBilletesDeCien = this.cantidadBilletesDeCien - (int)cantidadBilletesCien;

                        this.mensaje = "El monto dispensado es: " + montoDispensado + ", la cantidad de billetes de 1000 dispensados es: "
                            + cantidadBilletesMil + ", la cantidad de billetes de 500 es: " + cantidadBilletesQuinientos
                            + ", la cantidad de billetes de 100 dispensados es: " + cantidadBilletesCien;

                    }
                    else if ((this.montoSolicitado < 1000) && (this.EstanTodosLosBilletes()))
                    {
                        int cantidadBilletesMil = 0;
                        int cantidadBilletesQuinientos = 0;
                        float cantidadBilletesCien = 0;
                        double montoDispensado = this.montoSolicitado;

                        if (this.montoSolicitado >= 500)
                        {
                            cantidadBilletesQuinientos = 1;
                        }
                        else
                        {
                            cantidadBilletesQuinientos = 0;
                        }

                        if (this.montoSolicitado < 500)
                        {
                            cantidadBilletesCien = (float)Math.Round(this.montoSolicitado / 100, 1);
                        }
                        else
                        {
                            cantidadBilletesCien = (float)Math.Round(this.montoSolicitado - 500, 1) / 100;
                        }

                        this.montoDisponible = this.montoDisponible - montoDispensado;
                        this.cantidadBilletesDeMil = this.cantidadBilletesDeMil - cantidadBilletesMil;
                        this.cantidadBilletesDeQuinientos = this.cantidadBilletesDeQuinientos - cantidadBilletesQuinientos;
                        this.cantidadBilletesDeCien = this.cantidadBilletesDeCien - (int)cantidadBilletesCien;

                        this.mensaje = "El monto dispensado es: " + montoDispensado + ", la cantidad de billetes de 1000 dispensados es: "
                            + cantidadBilletesMil + ", la cantidad de billetes de 500 es: " + cantidadBilletesQuinientos
                            + ", la cantidad de billetes de 100 dispensados es: " + cantidadBilletesCien;
                    }
                }
            }
            
        }

        public bool EstanTodosLosBilletes()
        {
            int sumaDeBilletes = this.cantidadBilletesDeMil + this.cantidadBilletesDeQuinientos + this.cantidadBilletesDeCien;

            if (sumaDeBilletes == 127)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
