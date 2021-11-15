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
        private ITelegramBotClient bot;

        private TelegramBot()
        {
            this.bot = new TelegramBotClient(TELEBRAM_BOT_TOKEN);
        }

        public ITelegramBotClient Client
        {
            get
            {
                return this.bot;
            }
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
            bot.OnMessage += OnMessage;
            bot.StartReceiving();
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

            UserInfo userInfo = SessionRelated.Instance.GetUserById(chatId);

            if (userInfo == null)
            {
                SessionRelated.Instance.AddNewUser(new UserInfo(message.Chat.FirstName, chatId, new RoleDefault()));
            }

            Bot.Message msg = new Bot.Message(chatId, message.Text);

            string response;
            IHandler result = handler.Handle(msg, out response); // TODO try catch exceptions con mensajes
            // TODO Poder cancelar cosas en los handlers
            if (result == null)
            {
                bot.SendTextMessageAsync(chatId, "Disculpa no te entiendo");
            }
            else
            {
                bot.SendTextMessageAsync(chatId, response);
            }
        }
    }
}