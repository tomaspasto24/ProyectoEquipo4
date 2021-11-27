using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Bot;

namespace Library
{
    public class TelegramBot : AbstractBot
    {
        private const string TELEBRAM_BOT_TOKEN = "2100960953:AAGqylH0OVd18h5dJOPPZ0orCZOk6T4Wf9s";
        private static TelegramBot instance;
        //TODO !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! CREAR CADENA NUEVA DE HANDLERS 
        private readonly IHandler handler =
                        new TextNullHandler(
                        new CancelHandler(
                        new AddMaterialHandler(
                        
                        new CommandHandler(
                        new ContactHandler(
                        
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
                        new DefaultHandler(null)
        )))))))))))))))))))));

        private TelegramBot()
        {
            this.Client = new TelegramBotClient(TELEBRAM_BOT_TOKEN);
        }

        public ITelegramBotClient Client { get; private set; }

        private User BotInfo
        {
            get
            {
                return this.Client.GetMeAsync().Result;
            }
        }

        public int BotId
        {
            get
            {
                return this.BotInfo.Id;
            }
        }

        public string BotName
        {
            get
            {
                return this.BotInfo.FirstName;
            }
        }

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

        public override void StartCommunication()
        {
            Client.OnMessage += OnMessage;
            Client.StartReceiving();
        }

        public override void SendMessage(long id, string text)
        {
            Client.SendTextMessageAsync(id, text);
        }

        private void OnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            Telegram.Bot.Types.Message message = messageEventArgs.Message;
            long chatId = message.Chat.Id;
            Bot.Message msg = new Bot.Message(chatId, message.Text);


            UserInfo userInfo = SessionRelated.Instance.GetUserById(chatId);

            if (userInfo == null)
            {
                SessionRelated.Instance.AddNewUser(new UserInfo(message.Chat.FirstName, chatId, new RoleAdmin()));
            }

            string response;
            IHandler result;
            try
            {
                result = handler.Handle(msg, out response);
            }
            catch (System.Exception e)
            {
                SendMessage(chatId, $"Ha sucedido un error: {e.Message}");
                return;
            }

            SendMessage(chatId, response);
        }
    }
}