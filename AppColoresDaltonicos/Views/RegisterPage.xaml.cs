using AppColoresDaltonicos.Models.Auht;
using AppColoresDaltonicos.Services.Api;
using AppColoresDaltonicos.Services.Auth;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace AppColoresDaltonicos.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

	private async void OnRegisterClicked(object sender, EventArgs e)
	{
        string nombre = NombreEntry.Text;
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
		string passwordAgain = PasswordConfirmacio.Text;

		if (password.Length < 6)
		{
			var toast = Toast.Make("La contraseña  tiene que ser mas larga", ToastDuration.Short, 14);
			await toast.Show();
			return;
		}

		if (password != passwordAgain)
		{
			var toast = Toast.Make("Las contraseñas tiene que ser iguales", ToastDuration.Short, 14);
			await toast.Show();
			return;
		}

        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(nombre))
		{
			var toast = Toast.Make("El correo es obligatorio", ToastDuration.Short, 14);
			await toast.Show();
			return;
		}


		var nuevoUsuario = new RegisterRequestDto()
		{
			Name = nombre,
			Email = email,
			Password = password,
		};

		var apiService = Application.Current.MainPage.Handler.MauiContext.Services.GetService<IApiService>();
		var authService = Application.Current.MainPage.Handler.MauiContext.Services.GetService<IAuthService>();	


		await Navigation.PushAsync(new ConfiguracioVisualPage(nuevoUsuario, apiService, authService));

    }

	
}