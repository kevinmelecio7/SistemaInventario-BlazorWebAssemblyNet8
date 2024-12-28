namespace AppInvWebClient.Modals
{
    public class Modal : IModal
    {
        public bool showModal { get; set; } = false;
        public string MessageModal { get; set; } = string.Empty;
        public string TitleModal { get; set; } = string.Empty;
        public string MessageType { get; set; } = string.Empty;
        public string ModalIcon { get; set; } = string.Empty;
        public string MessageSecondary { get; set; } = string.Empty;

        public void Hide()
        {
            MessageModal = string.Empty;
            TitleModal = string.Empty;
            MessageType = string.Empty;
            ModalIcon = string.Empty;
            MessageSecondary = string.Empty;
            showModal = false;
        }

        public void Show(string message = "", string title = "", string type = "", string icon = "", string messageSecundary = "")
        {
            MessageModal = message;
            TitleModal = title;
            MessageType = type;
            ModalIcon = icon;
            MessageSecondary = messageSecundary;
            showModal = true;
        }

    }
}
