namespace MayorDeEdadCRUD
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbsService;
        private int _editResultadoId;

        public MainPage(LocalDbService localDbService)
        {
            InitializeComponent();
            _dbsService = localDbService;
            Task.Run(async () => Listview.ItemsSource = await _dbsService.GetResultado());
        }

        private async void determinarBtn_Clicked(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(EntryNombre.Text))
            {
                if (double.TryParse(EntryEdad.Text, out double edad))
                {

                    if (edad >= 18)
                    {
                        labelLey.Text = edad.ToString("Es mayor de edad");
                    }
                    else
                    {
                        labelLey.Text = edad.ToString("Es menor de edad");
                    }
                      Listview.ItemsSource = await _dbsService.GetResultado();
                }
                else
                {
                    await DisplayAlert("Error", "Escriba su edad", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Error", "Escriba su nombre", "Ok");
            }
          


            if (_editResultadoId == 0)
            {
                //agregar cliente
                await _dbsService.Create(new Determinar
                {
                    Nombre = EntryNombre.Text,
                    Edad = EntryEdad.Text,
                    Ley = labelLey.Text

                });
            }
            else
            {
                //Editar cliente
                await _dbsService.Update(new Determinar
                {
                    Id = _editResultadoId,
                    Nombre = EntryNombre.Text,
                    Edad = EntryEdad.Text,
                    Ley = labelLey.Text
                });
                _editResultadoId = 0;
            }

            EntryNombre.Text = string.Empty;
            EntryEdad.Text = string.Empty;
            labelLey.Text = string.Empty;


            
        }

        private async void Listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var determinar = (Determinar)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editResultadoId = determinar.Id;
                    EntryNombre.Text = determinar.Nombre;
                    EntryEdad.Text = determinar.Edad;
                    labelLey.Text = determinar.Ley;
                    break;

                case "Delete":
                    await _dbsService.Delete(determinar);
                    Listview.ItemsSource = await _dbsService.GetResultado();
                    break;

            }
        }
    }

}
