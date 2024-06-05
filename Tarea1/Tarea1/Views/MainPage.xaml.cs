using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tarea1.Models;
using Xamarin.Forms;

namespace Tarea1
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Operaciones> operaciones;
        public MainPage()
        {
            InitializeComponent();
            LoadOperaciones();
        }

        private async void BtnCalcular_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (double.TryParse(numero1.Text, out double numero1V) && double.TryParse(numero2.Text, out double numero2V))
                {
                    if (operationPicker.SelectedItem != null)
                    {
                        var operaciones = new Operaciones
                        {
                            numero1 = numero1V,
                            numero2 = numero2V,
                            operacion = operationPicker.SelectedItem.ToString(),
                            fechaRegistro = DateTime.Now

                        };

                        double resultado = 0;
                        string operacion = operaciones.operacion;

                        switch (operacion)
                        {
                            case "Sumar":
                                resultado = operaciones.numero1 + operaciones.numero2;
                                break;
                            case "Restar":
                                resultado = operaciones.numero1 - operaciones.numero2;
                                break;
                            case "Multiplicar":
                                resultado = operaciones.numero1 * operaciones.numero2;
                                break;
                            case "Dividir":
                                if (operaciones.numero2 != 0)
                                {
                                    resultado = operaciones.numero1 / operaciones.numero2;
                                }
                                else
                                {
                                    resultadoLabel.Text = "Error: No existe la division por cero";
                                    return;
                                }
                                break;
                            default:
                                resultadoLabel.Text = "Seleccione una operación válida";
                                return;
                        }
                        operaciones.total = resultado;

                        var result = await App.DataBaseConnection.Insertar(operaciones);
                        if (result == 1)
                        {
                            await DisplayAlert("Listo", "Se guardó su cálculo", "Aceptar");
                            numero1.Text = string.Empty;
                            numero2.Text = string.Empty;
                            operationPicker.SelectedIndex = -1;
                            LoadOperaciones();
                        }
                        else
                        {
                            await DisplayAlert("Error", "No se pudo guardar", "Aceptar");
                        }
                    }
                    else
                    {
                        resultadoLabel.Text = "Seleccione una operación";
                    }
                }
                else
                {
                    resultadoLabel.Text = "Ingrese números válidos";
                }

            }
            catch (Exception ex)
            {
                resultadoLabel.Text = "Error: " + ex.Message;
            }
        }

        private async void LoadOperaciones()
        {
            var operacionesList = await App.DataBaseConnection.Listar();
            operaciones = new ObservableCollection<Operaciones>(operacionesList);
            OperacionesListView.ItemsSource = operaciones;
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var op = button?.BindingContext as Operaciones;
            if (op != null)
            {
                bool confirm = await DisplayAlert("Confirmar eliminación", "¿Estás seguro de que deseas eliminar esta tarea?", "Sí", "No");
                if (confirm)
                {
                    await App.DataBaseConnection.Eliminar(op);
                    operaciones.Remove(op);
                }
            }
        }
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null; // Desmarca la tarea seleccionada
        }
    }
}
