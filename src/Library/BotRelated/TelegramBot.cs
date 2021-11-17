using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Bot;

namespace Library
{
    public class TelegramBot
    {

        private const string TELEBRAM_BOT_TOKEN = "2100960953:AAGqylH0OVd18h5dJOPPZ0orCZOk6T4Wf9s";
        private static TelegramBot instance;

        private TelegramBot()
        {
            this.Client = new TelegramBotClient(TELEBRAM_BOT_TOKEN);
        }

        public ITelegramBotClient Client
        {
            get;
        }

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

        public void StartCommunication()
        {
            Client.OnMessage += OnMessage;
            Client.StartReceiving();
        }

        private void OnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            IHandler handler =
                new CommandHandler(
                    new ContactHandler(
                        new ConvertUserToEntrepreneurHandler(
                            new PublishHandler(
                                new RegisterHandler(
                                    new ReportHandler(
                                        new SearchHandler(
                                            new StartHandler(
                                                new TokenHandler(
                                                    new UserInformationHandler(null)
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                );

            Telegram.Bot.Types.Message message = messageEventArgs.Message;
            long chatId = message.Chat.Id;
            Bot.Message msg = new Bot.Message(chatId, message.Text);


            UserInfo userInfo = SessionRelated.Instance.GetUserById(chatId);

            if (userInfo == null)
            {
                SessionRelated.Instance.AddNewUser(new UserInfo(message.Chat.FirstName, chatId, new RoleDefault()));
            }

            string response;
            IHandler result;
            try
            {
                result = handler.Handle(msg, out response);
            }
            catch (System.Exception e)
            {
                Client.SendTextMessageAsync(chatId, $"Ha sucedido un error: {e.Message}");
                return;
            }

            // TODO Poder cancelar cosas en los handlers
            if (result == null)
            {
                Client.SendTextMessageAsync(chatId, "Disculpa no te entiendo");
            }
            else
            {
                Client.SendTextMessageAsync(chatId, response);
            }
        }
    }
}