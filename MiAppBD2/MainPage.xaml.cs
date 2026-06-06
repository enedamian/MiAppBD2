using MiAppBD2.Models;
using MiAppBD2.Services;

namespace MiAppBD2
{
    public partial class MainPage : ContentPage
    {
        private readonly TareaRepository _tareaRepo;

        // Inyectamos el Repositorio
        public MainPage(TareaRepository tareaRepo)
        {
            InitializeComponent();
            _tareaRepo = tareaRepo;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarTareas();
        }

        private void CargarTareas()
        {
            // AHORA LLAMAMOS AL MÉTODO QUE HACE EL JOIN
            listaTareas.ItemsSource = _tareaRepo.ObtenerTareasConSuCategoria();
        }

        private async void OnNuevaTareaClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(TareaPage));
        }


        private async void OnEditarClicked(object sender, EventArgs e)
        {
            var boton = (Button)sender;
            // Ahora el botón nos devuelve el DTO (TareaConCategoria)
            var tareaDTO = (TareaConCategoriaDTO)boton.CommandParameter;

            // Buscamos la Tarea original en la base de datos usando la PK
            var tareaReal = _tareaRepo.ObtenerTareaPorId(tareaDTO.Id);

            var parametros = new Dictionary<string, object>
        {
            { "ObjetoTarea", tareaReal }
        };
            await Shell.Current.GoToAsync(nameof(TareaPage), parametros);
        }

        private async void OnEliminarClicked(object sender, EventArgs e)
        {
            bool confirmar = await DisplayAlertAsync("Borrar", "¿Estás seguro?", "Sí", "No");
            if (confirmar)
            {
                var boton = (Button)sender;
                var tareaDTO = (TareaConCategoriaDTO)boton.CommandParameter;

                // Para eliminar, a SQLite solo le importa el ID
                var tareaReal = new Tarea { Id = tareaDTO.Id };

                _tareaRepo.EliminarTarea(tareaReal);
                CargarTareas();
            }
        }
    }

}
