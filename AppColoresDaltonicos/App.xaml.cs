using AppColoresDaltonicos.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace AppColoresDaltonicos
{
    public partial class App : Application
    {
        private readonly IAuthService _authService;
        public App(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;

            MainPage = new ContentPage { Title = "Cargando...."};
        }


        protected override async void OnStart()
        {
            base.OnStart();
            var isAuthenticated = await _authService.IsTokenValidateAsync();
            if (isAuthenticated)
            {
                MainPage = new NavigationPage();
            }
            else
            {
                
            }
        }
    }
}