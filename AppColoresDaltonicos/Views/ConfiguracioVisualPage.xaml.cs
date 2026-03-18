using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using AppColoresDaltonicos.Models.Auht;
using AppColoresDaltonicos.Services.Api;
using AppColoresDaltonicos.Services.Auth;
using System.Diagnostics;

namespace AppColoresDaltonicos.Views;

public partial class ConfiguracioVisualPage : ContentPage
{
	private RegisterRequestDto _registerRequestDto;
    private readonly IApiService _apiService;
    private readonly IAuthService _authService;
    public ConfiguracioVisualPage(RegisterRequestDto usuario, IApiService apiService, IAuthService authService)
	{
		InitializeComponent();
		_registerRequestDto = usuario;
        _authService = authService;
        _apiService = apiService;

        TipoDaltonismoPicker.SelectedIndex = 0;
    }

	private void OnTipoDaltonismoChanged(object sender, EventArgs e)
	{
		if (TipoDaltonismoPicker.SelectedIndex == -1) return;
		
		string tipo = TipoDaltonismoPicker.SelectedItem.ToString();

        switch (tipo)
        {
            case "Protanopia":
                FiltroImagenReal.Source = "fotosrealistacolores_protanopia.png";
                break;
            case "Deuteranopia":
                FiltroImagenReal.Source = "fotosrealistacolores_deuteranopia.png";
                break;
            case "Tricromacia":
                FiltroImagenReal.Source = "fotosrealistacolores_tritanopia.png";
                break;
            case "Acromatopsia":
                FiltroImagenReal.Source = "fotosrealistacolores_acromatopsia.png";
                break;
        }

    }

	public void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
	{
		int valorRedondeado = (int)Math.Round(e.NewValue);
		PorcentajeLabe.Text = $"{valorRedondeado}%";

		FiltroImagenReal.Opacity = e.NewValue / 100.0;
    }

	public async void OnGuardarButtonClicked(object sender, EventArgs e)
    {
        try
        {
            GaurdarButton.IsEnabled = false;

            
            var authResponse = await _apiService.PostAsync<RegisterRequestDto, AuthResponseDto>("api/Usuario/registrar", _registerRequestDto);
            
            await _authService.GuardarTokenAsync(authResponse.Token);

            var configData = new
            {
                TipoDaltonismo = TipoDaltonismoPicker.SelectedItem.ToString(),
                Correccion = (int)Math.Round(CorrecionSlider.Value)
            };

            int usuarioId = authResponse.Usuario.Id;
            await _apiService.PutAsync<object, object>($"api/ConfiguracionDaltonismo/Usuario/{usuarioId}", configData);

            Application.Current.MainPage = new AppShell();
        }
        catch (Exception ex)
        {
            var toast = Toast.Make("Error al registrar el usuario", ToastDuration.Short, 14);
            Debug.WriteLine($"Este es el error {ex.Message}");
            await toast.Show();
            GaurdarButton.IsEnabled = true;
        }
	}
}