using System;
using Bot;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace Library
{
    /// <summary>
    /// Implementacion de AbstractBot que utiliza Telegram como interfaz de comunicacion
    /// Patrones y principios:
    /// Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP.
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    /// Cumple con Polymorphism porque usa los métodos polimórficos StartCommunication y SendMessage.
    /// Cumple con el patrón Singleton, esto lo que hace es que, garantiza que haya una única instancia de la clase y de esta forma se obtiene
    /// un acceso global a esta instancia.
    /// </summary>
    public class TelegramBot : AbstractBot
    {
        private const string TELEBRAM_BOT_TOKEN = "2100960953:AAGqylH0OVd18h5dJOPPZ0orCZOk6T4Wf9s";
        private static TelegramBot instance;
        private readonly IHandler handler =
                new TextNullHandler(
                new CancelHandler(
                new AddMaterialHandler(
                new AdminHandler(
                new CommandHandler(
                new ContactHandler(
                new DefaultRoleHandler(
                new ModifyEntrepreneurInformationHandler(
                new ModifyUserUbicationHandler(
                new ModifyUserHeaderHandler(
                new ModifyUserSpecializationsHandler(
                new ModifyUserCertificationsHandler(
                new PublishHandler(
                new PurchasesReportHandler(
                new RegisterHandler(
                new SalesReportHandler(
                new SearchHandler(
                new SearchByLocationHandler(
                new SearchByMaterialHandler(
                new StartHandler(
                new TokenHandler(
                new UndertakeHandler(
                new UserInformationHandler(
                new DefaultHandler(null))))))))))))))))))))))));

        /// <summary>
        /// Constructor por defecto, privado para facilitar la implementación del patron Singleton.
        /// Crea una nueva instancia de esta clase, creando una nueva instancia dentro de la API de telegram.
        /// </summary>
        private TelegramBot()
        {
            this.Client = new TelegramBotClient(TELEBRAM_BOT_TOKEN);
        }

        private ITelegramBotClient Client { get; set; }

        private User BotInfo
        {
            get
            {
                return this.Client.GetMeAsync().Result;
            }
        }

        /// <summary>
        /// Obtiene la única instancia de esta clase.
        /// </summary>
        /// <value>La única instancia de esta clase.</value>
        public static TelegramBot Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TelegramBot();
                }

                return instance;
            }
        }
        
        /// <summary>
        /// Comienza la comunicacion entre el bot y los usuarios.
        /// </summary>
        public override void StartCommunication()
        {
            this.Client.OnMessage += OnMessage;
            this.Client.StartReceiving();
        }

        /// <summary>
        /// Envia un mensaje al usuario con el bot como emisor.
        /// </summary>
        /// <param name="id">Id del usuario destinatario.</param>
        /// <param name="text">Mensaje a enviar.</param>
        public override void SendMessage(long id, string text)
        {
            this.Client.SendTextMessageAsync(id, text);
        }

        private void OnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            Telegram.Bot.Types.Message message = messageEventArgs.Message;
            long chatId = message.Chat.Id;
            Bot.Message msg = new Bot.Message(chatId, message.Text);
            UserInfo userInfo = SessionRelated.Instance.GetUserById(chatId);
            if (userInfo == null)
            {
                SessionRelated.Instance.AddNewUser(new UserInfo(message.Chat.FirstName, chatId));
            }

            string response;
            IHandler result;
            try
            {
                result = this.handler.Handle(msg, out response);
            }
            catch (Exception e)
            {
                SendMessage(chatId, $"Ha sucedido un error: {e.Message}");
                Console.WriteLine(e.ToString());
                return;
            }

            SendMessage(chatId, response);
        }
    }
}