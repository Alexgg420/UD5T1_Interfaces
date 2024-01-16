using System.Globalization;

namespace UD5T1
{
    public partial class MainPage : ContentPage
    {
        decimal cuenta;
        int propina;
        int personas;

        public MainPage()
        {
            InitializeComponent();
        }

        private void CalcularTotal()
        {
            if (personas > 0)
            {

                // Propina total
                var propinaTotal = cuenta * propina / 100;

                // Propina por persona
                var propinaPorPersona = propinaTotal / personas;
                lblPropinaPorPersona.Text = $"{propinaPorPersona:C}";

                // Subtotal
                var subtotal = cuenta / personas;
                lblSubtotal.Text = $"{subtotal:C}";

                // Total
                var totalPorPersona = (cuenta + propinaTotal) / personas;
                lblTotal.Text = $"{totalPorPersona:C}";
            }
        }

        private void TxtCuenta_Completed(object sender, EventArgs e)
        {
            try
            {
                var cuenta = txtCuenta.Text;
                CultureInfo cultura = new CultureInfo("en-EN");
                this.cuenta = decimal.Parse(cuenta, cultura);
                CalcularTotal();
            }
            catch (FormatException)
            {
                DisplayAlert("Error", "Ingrese un valor válido para la cuenta.", "OK");
            }
        }


        private void SldPropina_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            propina = (int)e.NewValue;

            lblPropina.Text = $"{propina}%";

            CalcularTotal();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                var btn = (Button)sender;
                var porcentaje = int.Parse(btn.Text.Replace("%", string.Empty));
                sldPropina.Value = porcentaje;
            }
        }

        private void BtnMenos_Clicked(object sender, EventArgs e)
        {
            if(personas > 1)
            {
                personas = personas - 1;

                lblPersonas.Text = $"{personas}";

                CalcularTotal() ;
            }
        }

        private void BtnMas_Clicked(object sender, EventArgs e)
        {
            personas = personas + 1;

            lblPersonas.Text = $"{personas}";

            CalcularTotal();
        }
    }
}
