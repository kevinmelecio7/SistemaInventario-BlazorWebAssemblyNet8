namespace AppInvWebClient.Modals
{
    public interface IModal
    {
        public bool showModal { get; set; }
        public string MessageModal { get; set; }
        public string TitleModal { get; set; }
        public string MessageType { get; set; }
        public string ModalIcon { get; set; }
        public string MessageSecondary { get; set; }

        public void Show(string message = "", string title = "", string type = "", string icon = "", string messageSecundary = "");
        public void Hide();
    }
}
