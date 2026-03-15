using AppColoresDaltonicos.Models.Auht;

namespace AppColoresDaltonicos.Views;

public partial class ConfiguracioVisualPage : ContentPage
{
	private RegisterRequestDto _registerRequestDto;
    public ConfiguracioVisualPage(RegisterRequestDto usuario)
	{
		InitializeComponent();
		_registerRequestDto = usuario;
	}

	public void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
	{
		int valorRedondeado = (int)Math.Round(e.NewValue);
		PorcentajeLabe.Text = $"{valorRedondeado}";
    }

	public async void OnGuardarButtonClicked(object sender, EventArgs e)
	{
		return;
	}

	
}