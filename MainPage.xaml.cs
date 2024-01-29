///////////////////////////////////////////
// Tarea: UD5T1
// Alumno/a: Alejandro Giráldez Guerrero
// Curso: 2023/2024
///////////////////////////////////////////

using System.Globalization;

namespace UD5T1
{
    /// <summary>
    /// La clase MainPage es la página de backend por así decirlo, pues
    /// en esta se encuentran las funciones que usará el programa para
    /// funcionar correctamente
    /// </summary>
    public partial class MainPage : ContentPage
    {
        /// <param name="cuenta">
        /// Esta es la variable o parámetro que guarda
        /// el precio final mostrado a pagar, ya sea de forma conjunta o 
        /// individual, con o sin propina
        /// </param>
        decimal cuenta;
        /// <param name="propina">
        /// Esta es la variable o parámetro que guarda
        /// la propina indicada por el cliente, incluso cuando es cero
        /// </param>
        int propina;
        /// <param name="personas">
        /// Esta es la variable o parámetro que guarda
        /// la cantidad de personas que han participado en la suma total de la
        /// cuenta. El mínimo posible es 1 persona
        /// </param>
        int personas;

        /// <summary>
        /// Este es el constructor de la Clase MainPage.
        /// Lo único indicado en este es que se inicie el componente
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// La función Calcular total se encarga de calcular el precio
        /// en función de los parámetros indicados, tales como la propina
        /// o la cantidad de personas entre las que dividir el total
        /// </summary>
        private void CalcularTotal()
        {
            if (personas > 0)
            {
                CultureInfo cultura = new CultureInfo("en-US");
                // Propina total
                var propinaTotal = cuenta * propina / 100;

                // Propina por persona
                var propinaPorPersona = propinaTotal / personas;
                lblPropinaPorPersona.Text = $"{propinaPorPersona.ToString("C", cultura)}";

                // Subtotal
                var subtotal = cuenta / personas;
                lblSubtotal.Text = $"{subtotal.ToString("C", cultura)}";

                // Total
                var totalPorPersona = (cuenta + propinaTotal) / personas;
                lblTotal.Text = $"{totalPorPersona.ToString("C", cultura)}";
            }
        }

        /// <summary>
        /// La función TxtCuenta_Completed muestra como texto la cuenta del pedido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// La función SldPropina_ValueChanged cambia el valor de la propina
        /// cuando el usuario lo indica en el programa, arrastrando la barra horizontal
        /// para hacer que el valor de la propina cambie hasta la cantidad deseada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SldPropina_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            propina = (int)e.NewValue;

            lblPropina.Text = $"{propina}%";

            CalcularTotal();
        }

        /// <summary>
        /// La función Button_Clicked cambia el valor de la propina
        /// cuando el usuario pulsa uno de los tres botones con porcentajes que indican la cantidad
        /// de propina que desea añadir el cliente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                var btn = (Button)sender;
                var porcentaje = int.Parse(btn.Text.Replace("%", string.Empty));
                sldPropina.Value = porcentaje;
            }
        }

        /// <summary>
        /// La función BtnMenos_Clicked disminuye la cantidad de personas
        /// entre las que dividir el total de la cuenta. El mínimo posible
        /// es una persona, por lo tanto no disminuirá a un valor menor a 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMenos_Clicked(object sender, EventArgs e)
        {
            if(personas > 1)
            {
                personas = personas - 1;

                lblPersonas.Text = $"{personas}";

                CalcularTotal() ;
            }
        }

        /// <summary>
        /// La función BtnMas_Clicked aumenta la cantidad de personas
        /// entre las que dividir el total de la cuenta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMas_Clicked(object sender, EventArgs e)
        {
            personas = personas + 1;

            lblPersonas.Text = $"{personas}";

            CalcularTotal();
        }
    }
}
