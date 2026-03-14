namespace AppColoresDaltonicos.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
		string email = EmailEntry.Text;
		string password = PasswordEntry.Text;

		if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
		{
			await DisplayAlert("Error", "Por favor, ingresa tu correo electrónico y contraseña.", "OK");
			return;
		}

		CargandoIndicator.IsRunning = true;
		CargandoIndicator.IsVisible = true;
		LoginButton.IsEnabled = false;

		await Task.Delay(2000);

        CargandoIndicator.IsRunning = false;
		CargandoIndicator.IsVisible = false;
		LoginButton.IsEnabled = true;

		await DisplayAlert("Éxito", "¡Has iniciado sesión correctamente!", "OK");
    }

	private async void OnIrARegistroTapped(object sender, TappedEventArgs e)
	{
		await Navigation.PushAsync(new RegisterPage());
    }
}