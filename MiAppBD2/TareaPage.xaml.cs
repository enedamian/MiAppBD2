using MiAppBD2.Models;
using MiAppBD2.Services;

namespace MiAppBD2;

[QueryProperty(nameof(TareaAEditar), "ObjetoTarea")]
public partial class TareaPage : ContentPage
{
    private readonly TareaRepository _tareaRepo;
    private Tarea _tareaActual;
    private readonly CategoriaRepository _catRepo; // Repositorio nuevo
    private List<Categoria> _categorias;

    public Tarea TareaAEditar
    {
        set
        {
            _tareaActual = value;
            txtTitulo.Text = _tareaActual.Titulo;

            // Si la tarea tiene una fecha guardada (No es NULL ni vacía)
            if (!string.IsNullOrEmpty(_tareaActual.FechaLimite))
            {
                chkTieneFecha.IsChecked = true;
                dpFecha.IsVisible = true;
                dpFecha.Date = DateTime.Parse(_tareaActual.FechaLimite); // Convertimos el texto a Fecha
            }
            else
            {
                chkTieneFecha.IsChecked = false;
                dpFecha.IsVisible = false;
            }

            // SELECCIONAR LA CATEGORÍA EN EL PICKER
            if (_tareaActual.CategoriaId != 0)
            {
                // Buscamos en nuestra lista la categoría que coincida con la FK de la tarea
                pckCategoria.SelectedItem = _categorias.FirstOrDefault(c => c.id == _tareaActual.CategoriaId);
            }


        }
    }

    // Inyectamos el Repositorio desde el constructor
    public TareaPage(TareaRepository tareaRepo, CategoriaRepository catRepo)
    {
        InitializeComponent();
        _tareaRepo = tareaRepo;
        _catRepo = catRepo;
        _tareaActual = new Tarea();

        // CARGAMOS EL PICKER: Traemos las categorías de la BD y las ponemos en la UI
        _categorias = _catRepo.ObtenerCategorias();
        pckCategoria.ItemsSource = _categorias;


    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtTitulo.Text))
        {
            await DisplayAlertAsync("Error", "El título es obligatorio", "OK");
            return;
        }

        _tareaActual.Titulo = txtTitulo.Text;

        // LÓGICA DEL ALMANAQUE HACIA LA BASE DE DATOS
        if (chkTieneFecha.IsChecked && dpFecha.Date.HasValue)
        {
            // Convertimos la fecha seleccionada a texto formato Año-Mes-Día para SQLite
            _tareaActual.FechaLimite = dpFecha.Date.Value.ToString("yyyy-MM-dd");
        }
        else
        {
            // Si no está marcado, forzamos un NULL para que el ORDER BY de SQLite funcione bien
            _tareaActual.FechaLimite = null;
        }
        // ASIGNAR LA FOREIGN KEY
        var catSeleccionada = (Categoria)pckCategoria.SelectedItem;
        if (catSeleccionada != null)
        {
            _tareaActual.CategoriaId = catSeleccionada.id; // Guardamos la PK de la categoría
        }

        // Guardamos usando el repositorio
        _tareaRepo.GuardarTarea(_tareaActual);

        await Shell.Current.GoToAsync("..");
    }

    private void OnTieneFechaChanged(object sender, CheckedChangedEventArgs e)
    {
        // Muestra u oculta el calendario según si la casilla está marcada
        dpFecha.IsVisible = chkTieneFecha.IsChecked;
    }
}

