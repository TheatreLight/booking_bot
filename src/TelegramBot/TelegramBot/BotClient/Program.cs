using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using menu = Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup;
using button = Telegram.Bot.Types.ReplyMarkups.KeyboardButton;
using Telegram.Bot.Polling;
using System.Net;
using Update = Telegram.Bot.Types.Update;
using Telegram.Bot.Types.Enums;



namespace BotClient
{
    
    class Program
    {



        public static List<string> GetSubject(string str) 
        {

            List<string> list = new List<string>();
            string url = "http://localhost:5000/api/"+str;
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Accept = "application/json";
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
 
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd().Split(",");
       
                foreach (var i in result) 
                {
                    
                    if (i.StartsWith("\"nameSubject")) 
                    {
                        list.Add(i.Substring(14).Trim('"'));
                    }

                }
 
            }

            Console.WriteLine(httpResponse.StatusCode);
            httpResponse.Close();
            return list;
        }


        static ITelegramBotClient botClient = new TelegramBotClient("5563204025:AAFmwaasYYNJVe4DtAYEI66yuVVqKKZ-IQU");
        public static int user_type = 0;
        public static string bookedObject;
        public static string bookedFloor;
        public static string bookDate;
        public static TimeSpan time;
        public static string[] week = new string[7];
        public static string output;

        public static List<string> input_student = new List<string>();
        public static List<string> input_stuff = new List<string>();
        public static List<string> input_time = new List<string>();



        



        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

            if (output == null)
            {   

                
                if (update.Type == UpdateType.Message)
                {
                    Message message = new Message();
                    await StartMessage(update.Message);

                    whoAreYouAsync(update.Message);
                    Book(update.Message);
                    LookBook(update.Message);
                    SubMenuBook(update.Message);
                    SubMenuDate(update.Message);
                    SubMenuTime(update.Message);
                }

                if (TimeSpan.TryParse(update.Message.Text, out time))
                {
                    output = "�� ������������ " + bookedObject + " " + bookedFloor + " �� " + bookDate + " " + time;
                    var data = new Booking()
                    {
                        SubjectId = 1,
                        UserId = 1,
                        Date = bookDate,
                        TimeFrom = time.ToString(),
                    };



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
                    reply(update.Message, "�����, �� ������ �������� ������ ���� ����� � ������...", null);
                }

            }


            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                await HandleCallbackQuery(botClient, update.CallbackQuery);
                return;
            }
        }
        private static void findName(string name) 
        {
            string url = "http://localhost:5000/api/users/"+name;
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Accept = "application/json";
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd().Split(",");
                Console.WriteLine(result);
                foreach (var i in result)
                {
                    Console.WriteLine(i);
                }

            }
            Console.WriteLine(httpResponse.StatusCode);
            httpResponse.Close();
        }


        public static async Task StartMessage(Message message)
        {
            if (message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat, "������! � - �������, � � ������ ���� ������������� ���-���� � ����� �����. �� ��� ������ �����, ��� ��?\n/student\n/stuff");

               

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
                    => $"������ ��\n{apiRequestExcepyion.ErrorCode}\n{apiRequestExcepyion.Message}",
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

        private static menu StudentBookMenu(List<string> input_student)
        {

          
            menu keyboard = new(new[]
            {
                
                new button[] {input_student[0], input_student[1], input_student[2]},
                new button[] { "Cancel booking" }
            }
            )
            { ResizeKeyboard = true };

            return keyboard;
        }

        private static menu stuffBookMenu(List<string> input_stuff)
        {
            menu keyboard = new(new[]
                {
                    new button[] { input_stuff[0], input_stuff[1], input_stuff[2], input_stuff[3] },
                    new button[] { input_stuff[3], input_stuff[4] },
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
                    new button[] { "18 ����", "20 ����" },
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
                    new button[] { "�����", "����������", "Playstation"},
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
                    new button[] { "17 ����", "18 ����", "20 ����"},
                    new button[] { "Cancel booking" }
                }
                )
            { ResizeKeyboard = true };
            return keyboard;
        }

        private static menu dateMenu()
        {
            DateTime date = DateTime.Now;
            for (int i = 0; i < 7; i++)
            {
                week[i] = (date.AddDays(i)).ToShortDateString();

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
        private static async Task whoAreYouAsync(Message message)
        {
           

            switch (message.Text.ToLower())
            {
                case "/student":
                    user_type = 1;
                    var keyboard = MainMenu();
                    reply(message, "������, �������!\n����� �� ������ ������������� ���-�� ���� ���������� ���� �����.", keyboard);
                    
                    break;
                case "/stuff":
                    user_type = 2;
                    keyboard = MainMenu();
                    reply(message, "������, �������!\n����� �� ������ ������������� ���-�� ���� ���������� ���� �����.", keyboard);
                    break;
                case "/admin":
                    user_type = 3;
                    keyboard = MainMenu();
                    reply(message, "������, ���������!\n�� ������ ����� ���������� ����� �� �������, �� ����, ��������� ����-�� ����� ��� �������� ��.", keyboard);
                    break;
                default:
                    /*                    botClient.SendTextMessageAsync(message.Chat, "� �� ���� ����.");
                    */
                    break;
            }
        }

        private static void Book(Message message)
        {
            if (message.Text == "Book" && user_type == 1)
            {

                //GetSubject("subject");

                var keyboard = StudentBookMenu(GetSubject("subject/sec/student"));
                reply(message, "�� ������ ������������� ��������� �������:", keyboard);
            }
            if (message.Text == "Book" && user_type == 2)
            {
                var keyboard = stuffBookMenu(GetSubject("subject/sec/staff"));
                reply(message, "�� ������ ������������� ��������� �������:", keyboard);
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
            if (message.Text == "Miting room")
            {
                bookedObject = message.Text;
                var keyboard = meetingMenu();
                reply(message, "������ ����!", keyboard);
            }

            if (message.Text == "�����-���������")
            {
                var keyboard = sportMenu();
                reply(message, "������, ��� ������.", keyboard);
            }

            if (message.Text == "����")
            {
                var keyboard = gameMenu();
                reply(message, "������, ��� ������.", keyboard);
            }

            if (message.Text == "�����")
            {
                bookedObject = message.Text;
                var keyboard = kitchenMenu();
                reply(message, "������ ����!", keyboard);
            }
        }

        private static void SubMenuDate(Message message)
        {
            if (message.Text == "���� 17 ��.")
            {
                bookedObject = message.Text;
                var keyboard = dateMenu();
                reply(message, "������ ����.", keyboard);
            }

            if (message.Text == "17 ����")
            {
                bookedFloor = message.Text;
                var keyboard = dateMenu();
                reply(message, "������ ����.", keyboard);
            }

            if (message.Text == "18 ����")
            {
                bookedFloor = message.Text;
                var keyboard = dateMenu();
                reply(message, "������ ����.", keyboard);
            }

            if (message.Text == "20 ����")
            {
                bookedFloor = message.Text;
                var keyboard = dateMenu();
                reply(message, "������ ����.", keyboard);
            }

            if (message.Text == "Ping-pong")
            {
                bookedObject = message.Text;
                var keyboard = dateMenu();
                reply(message, "������ ����.", keyboard);
            }

            if (message.Text == "�����")
            {
                bookedObject = message.Text;
                var keyboard = dateMenu();
                reply(message, "������ ����.", keyboard);
            }

            if (message.Text == "����������")
            {
                bookedObject = message.Text;
                var keyboard = dateMenu();
                reply(message, "������ ����.", keyboard);
            }

            if (message.Text == "Playstation")
            {
                bookedObject = message.Text;
                var keyboard = dateMenu();
                reply(message, "������ ����.", keyboard);
            }

            if (message.Text == "������")
            {
                bookedObject = message.Text;
                var keyboard = dateMenu();
                reply(message, "������ ����.", keyboard);
            }

            if (message.Text == "Play room")
            {
                bookedObject = message.Text;
                var keyboard = dateMenu();
                reply(message, "������ ����.", keyboard);
            }
        }

        private static void SubMenuTime(Message message)
        {
            if (message.Text == week[0] || message.Text == week[1] || message.Text == week[2] ||
                message.Text == week[3] || message.Text == week[4] || message.Text == week[5] || message.Text == week[6])
            {
                bookDate = message.Text;
                var keyboard = timeMenu();
                reply(message, "������ �����.", keyboard);
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
            input_student.Add("���� 17 ��.");
            input_student.Add("�����������");
            input_student.Add("�����-���������");
            input_student.Add("����");

            input_stuff.Add("���� 17 ��.");
            input_stuff.Add("�����������");
            input_stuff.Add("�����-���������");
            input_stuff.Add("����");
            input_stuff.Add("�����");
            input_stuff.Add("������");
            input_stuff.Add("����������");

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
            Console.WriteLine("������� ��� " + botClient.GetMeAsync().Result.FirstName);

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
