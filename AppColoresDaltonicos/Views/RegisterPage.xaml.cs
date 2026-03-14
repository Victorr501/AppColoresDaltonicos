namespace AppColoresDaltonicos.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

	private void OnRegisterClicked(object sender, EventArgs e)
	{
		// Aquí puedes agregar la lógica para registrar al usuario
		DisplayAlert("Registro", "Usuario registrado exitosamente", "OK");
    }

	private void GoBack(object sender, EventArgs e)
	{
		Navigation.PopAsync();
	}
}