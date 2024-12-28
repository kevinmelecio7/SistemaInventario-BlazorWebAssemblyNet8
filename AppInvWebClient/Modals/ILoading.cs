namespace AppInvWebClient.Modals
{
    public interface ILoading
    {
        public bool showLoading { get; set; }
        public string messageLoading { get; set; }
        public void Show(string message = "Cargando");
        public void Hide();
    }
}
