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

	private void OnTipoDaltonismoChanged(object sender, EventArgs e)
	{
		if (TipoDaltonismoPicker.SelectedIndex == -1) return;
		
		string tipo = TipoDaltonismoPicker.SelectedItem.ToString();

        switch (tipo)
        {
            case "Protanopia":
                FiltroBoxView.Color = Colors.Red;
                break;
            case "Deuteranopia":
                FiltroBoxView.Color = Colors.Green;
                break;
            case "Tricromacia":
                FiltroBoxView.Color = Colors.Blue;
                break;
            case "Acromatopsia":
                FiltroBoxView.Color = Colors.Yellow;
                break;
        }

    }

	public void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
	{
		int valorRedondeado = (int)Math.Round(e.NewValue);
		PorcentajeLabe.Text = $"{valorRedondeado}";

		FiltroBoxView.Opacity = e.NewValue / 100.0;
    }

	public async void OnGuardarButtonClicked(object sender, EventArgs e)
	{
		return;
	}

	
}