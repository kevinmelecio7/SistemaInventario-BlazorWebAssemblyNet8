namespace AppInvWebClient.Modals
{
    public class Loading : ILoading
    {
        public bool showLoading { get; set; } = false;
        public string messageLoading { get; set; } = "Cargando...";

        public void Hide()
        {
            messageLoading = string.Empty;
            showLoading = false;
        }

        public void Show(string message = "Cargando")
        {
            messageLoading = message;
            showLoading = true;
        }
    }
}
