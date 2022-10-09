using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;

using menu = Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup;
using button = Telegram.Bot.Types.ReplyMarkups.KeyboardButton;

namespace BotInterface
{
    class Program
    { 
        static ITelegramBotClient botClient = new TelegramBotClient("5650023738:AAFe2XezsfxD-znXXsuzOlhVYYWxYp3Jrik");
        public static string[] week = new string[7];
        public static string output;

        /* Output DATA */
        public static int user_type = 0;
        public static string bookedObject;
        public static string bookedFloor;
        public static string bookDate;
        public static TimeSpan time;
       

        /* Input DATA */
        public static List<string> input_student = new List<string>();  // Input lists of valid objects. If you want to use they - delete InitLists()!!!!!!!!!!!!!!
        public static List<string> input_stuff = new List<string>();
        public static List<string> input_time = new List<string>();
        public static string login;

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

            if (output == null)
            {

                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {
                    await StartMessage(botClient, update.Message);
                    login = update.Message.Text;  // variable for return users role from database!!!
                    whoAreYou(update.Message);
                    Book(update.Message);
                    LookBook(update.Message);
                    SubMenuBook(update.Message);
                    SubMenuDate(update.Message);
                    SubMenuTime(update.Message);
                }

                if (TimeSpan.TryParse(update.Message.Text, out time))
                {
                    output = "Ты забронировал " + bookedObject + " " + bookedFloor + " на " + bookDate + " " + time;
                
                }
                if (output != null)
                {
                    reply(update.Message, output, null);
                }
            }
            else
            {
                if (update.Message.Text == "/cancel" || update.Message.Text == "Cancel booking")
                {
                    output = null;
                }
                else
                {
                    reply(update.Message, output, null);
                    reply(update.Message, "Сорян, ты можешь получить только одну бронь в неделю...", null);
                }
                
            }


            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                await HandleCallbackQuery(botClient, update.CallbackQuery);
                return;
            }
        }

        public static async Task StartMessage(ITelegramBotClient botClient, Message message)
        {
            if (message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat, "Привет! Я - Виталик, и я помогу тебе забронировать что-либо в нашей школе. Но для начала скажи, кто ты?\n" +
                    "/student\n/stuff\n/admin");
                return;
            }
        }
        public static async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {

        }

        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestExcepyion
                    => $"Ошибка ТГ\n{apiRequestExcepyion.ErrorCode}\n{apiRequestExcepyion.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        /* KEYBOARDS-MENUS */

        private static menu MainMenu()
        {
            menu keyboard = new(new[]
                {
                    new button[] { "Book", "Look booking" },
                    new button[] { "Cancel booking" }
                }
                )
            { ResizeKeyboard = true };
            return keyboard;
        }

        private static menu StudentBookMenu(List<string> input)
        {
            menu keyboard = new(new[]
            {
                new button[] {input_student[0], input_student[1], input_student[2], input_student[3]},
                new button[] { "Cancel booking" }
            }
            )
            { ResizeKeyboard = true };
      
            return keyboard;
        }

        private static menu stuffBookMenu()
        {
            menu keyboard = new(new[]
                {
                    new button[] { input_stuff[0], input_stuff[1], input_stuff[2], input_stuff[3] },
                    new button[] { input_stuff[4], input_stuff[5], input_stuff[6] },
                    new button[] { "Cancel booking" }
                }
                )
            { ResizeKeyboard = true };
            return keyboard;
        }

        private static menu meetingMenu()
        {
            menu keyboard = new(new[]
                {
                    new button[] { "18 этаж", "20 этаж" },
                    new button[] { "Cancel booking" }
                }
                )
            { ResizeKeyboard = true };
            return keyboard;
        }

        private static menu sportMenu()
        {
            menu keyboard = new(new[]
                {
                    new button[] { "Ping-pong" },
                    new button[] { "Cancel booking" }
                }
                )
            { ResizeKeyboard = true };
            return keyboard;
        }

        private static menu gameMenu()
        {
            menu keyboard = new(new[]
                {
                    new button[] { "Кикер", "Аэрохоккей", "Playstation"},
                    new button[] { "Cancel booking" }
                }
                )
            { ResizeKeyboard = true };
            return keyboard;
        }

        private static menu kitchenMenu()
        {
            menu keyboard = new(new[]
                {
                    new button[] { "17 этаж", "18 этаж", "20 этаж"},
                    new button[] { "Cancel booking" }
                }
                )
            { ResizeKeyboard = true };
            return keyboard;
        }

        private static menu dateMenu()
        {
            DateTime date = DateTime.Now;

            
            for (int i = 0, j = 0; i < 7; i++, j++)
            {
                week[i] = (date.DayOfWeek + j).ToString();
                if ((int)date.DayOfWeek + j == 6)
                {
                    j = i - 7;
                }
            }

            menu keyboard = new(new[]
                {
                    new button[] { week[0], week[1], week[2], week[3] },
                    new button[] { week[4], week[5], week[6] },
                    new button[] { "Cancel booking" }

                }
                )
            { ResizeKeyboard = true };
            return keyboard;
        }

        private static menu timeMenu()
        {
            menu keyboard = new(new[]
               {
                    new button[] { input_time[0], input_time[1], input_time[2], input_time[3] },
                    new button[] { input_time[4], input_time[5], input_time[6], input_time[7] },
                    new button[] { input_time[8], input_time[9], input_time[10], input_time[11] },
                    new button[] { input_time[12], input_time[13], input_time[14], input_time[15] },
                    new button[] { input_time[12], input_time[13], input_time[14], input_time[15] },
                    new button[] { input_time[16], input_time[17], input_time[18], input_time[19] },
                    new button[] { input_time[21], input_time[22], input_time[23] },
                    new button[] { "Cancel booking" }

                }
               )
            { ResizeKeyboard = true };
            return keyboard;
        }

        /**/
        private static void whoAreYou(Message message)
        {
            switch (login)
            {
                case "/student":
                    user_type = 1;
                    var keyboard = MainMenu();
                    reply(message, "Привет, студент!\nЗдесь ты можешь забронировать что-то либо посмотреть свою бронь.", keyboard);
                    break;
                case "/stuff":
                    user_type = 2;
                    keyboard = MainMenu();
                    reply(message, "Привет, коллега!\nЗдесь ты можешь забронировать что-то либо посмотреть свою бронь.", keyboard);
                    break;
                default:
                    //botClient.SendTextMessageAsync(message.Chat, "Я не знаю тебя.");
                    break;
            }
        }

        private static void Book(Message message)
        {
            if (message.Text == "Book" && user_type == 1)
            {
                var keyboard = StudentBookMenu(input_student);
                reply(message, "Ты можешь забронировать следующие объекты:", keyboard);
            }
            if (message.Text == "Book" && user_type == 2)
            {
                var keyboard = stuffBookMenu();
                reply(message, "Ты можешь забронировать следующие объекты:", keyboard);
            }
        }

        private static void LookBook(Message message)
        {
            if (message.Text == "Look booking")
            {
                if (output != null)
                {
                    reply(message, output, null);
                }
                else
                {
                    reply(message, "You don't have any bookings yet", null);
                }
            }
        }

        private static void SubMenuBook(Message message)
        {
            if (message.Text == "Переговорка")
            {
                bookedObject = message.Text;
                var keyboard = meetingMenu();
                reply(message, "Выбери этаж!", keyboard);
            }

            if (message.Text == "Спорт-инвентарь")
            {
                var keyboard = sportMenu();
                reply(message, "Выбери, что хочешь.", keyboard);
            }

            if (message.Text == "Игры")
            {
                var keyboard = gameMenu();
                reply(message, "Выбери, что хочешь.", keyboard);
            }

            if (message.Text == "Кухня")
            {
                bookedObject = message.Text;
                var keyboard = kitchenMenu();
                reply(message, "Выбери этаж!", keyboard);
            }
        }

        private static void SubMenuDate(Message message)
        {
            if (message.Text == "Холл 17 эт.")
            {
                bookedObject = message.Text;
                var keyboard = dateMenu();
                reply(message, "Выбери день.", keyboard);
            }

            if (message.Text == "17 этаж")
            {
                bookedFloor = message.Text;
                var keyboard = dateMenu();
                reply(message, "Выбери день.", keyboard);
            }

            if (message.Text == "18 этаж")
            {
                bookedFloor = message.Text;
                var keyboard = dateMenu();
                reply(message, "Выбери день.", keyboard);
            }

            if (message.Text == "20 этаж")
            {
                bookedFloor = message.Text;
                var keyboard = dateMenu();
                reply(message, "Выбери день.", keyboard);
            }

            if (message.Text == "Ping-pong")
            {
                bookedObject = message.Text;
                var keyboard = dateMenu();
                reply(message, "Выбери день.", keyboard);
            }

            if (message.Text == "Кикер")
            {
                bookedObject = message.Text;
                var keyboard = dateMenu();
                reply(message, "Выбери день.", keyboard);
            }

            if (message.Text == "Аэрохоккей")
            {
                bookedObject = message.Text;
                var keyboard = dateMenu();
                reply(message, "Выбери день.", keyboard);
            }

            if (message.Text == "Playstation")
            {
                bookedObject = message.Text;
                var keyboard = dateMenu();
                reply(message, "Выбери день.", keyboard);
            }

            if (message.Text == "Шафран")
            {
                bookedObject = message.Text;
                var keyboard = dateMenu();
                reply(message, "Выбери день.", keyboard);
            }

            if (message.Text == "Мастерская")
            {
                bookedObject = message.Text;
                var keyboard = dateMenu();
                reply(message, "Выбери день.", keyboard);
            }
        }

        private static void SubMenuTime(Message message)
        {
            if (message.Text == week[0] || message.Text == week[1] || message.Text == week[2] ||
                message.Text == week[3] || message.Text == week[4] || message.Text == week[5] || message.Text == week[6])
            {
                bookDate = message.Text;
                var keyboard = timeMenu();
                reply(message, "Выбери время.", keyboard);
            }
        }
        private static void CancelBook(Message message)
        {
            if (message.Text == "Cancel booking")
            {
                output = "";
            }
        }

        async private static void reply(Message message, String greet, menu keyboard)
        {
            await botClient.SendTextMessageAsync(message.Chat, greet, replyMarkup: keyboard);
            return;
        }


        /* temp funcs */

        private static void InitLists(ref List<string> input_student, ref List<string> input_stuff, ref List<string> input_time)
        {
            input_student.Add("Холл 17 эт.");
            input_student.Add("Переговорка");
            input_student.Add("Спорт-инвентарь");
            input_student.Add("Игры");

            input_stuff.Add("Холл 17 эт.");
            input_stuff.Add("Переговорка");
            input_stuff.Add("Спорт-инвентарь");
            input_stuff.Add("Игры");
            input_stuff.Add("Кухня");
            input_stuff.Add("Шафран");
            input_stuff.Add("Мастерская");

            input_time.Add("0:00");
            input_time.Add("1:00");
            input_time.Add("2:00");
            input_time.Add("3:00");
            input_time.Add("4:00");
            input_time.Add("5:00");
            input_time.Add("6:00");
            input_time.Add("7:00");
            input_time.Add("8:00");
            input_time.Add("9:00");
            input_time.Add("10:00");
            input_time.Add("11:00");
            input_time.Add("12:00");
            input_time.Add("13:00");
            input_time.Add("14:00");
            input_time.Add("15:00");
            input_time.Add("16:00");
            input_time.Add("17:00");
            input_time.Add("18:00");
            input_time.Add("19:00");
            input_time.Add("20:00");
            input_time.Add("21:00");
            input_time.Add("22:00");
            input_time.Add("23:00");
        }


        static void Main(string[] args)
        {
            InitLists(ref input_student, ref input_stuff, ref input_time);
            Console.WriteLine("Запущен бот " + botClient.GetMeAsync().Result.FirstName);
            
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };
            botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
            cts.Cancel();
        }
    }

}