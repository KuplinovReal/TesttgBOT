using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using System.IO;

namespace SELL_PRIVATE_BOT
{
    class Program
    {
        static string token = "6301921155:AAH2fM3NowFjdOY7BOZdcTIrS69Ah9ExFCc";
        static long ID_own = 6787734266;         

        static void Main(string[] args)
        {
            var botClient = new TelegramBotClient(token);
            botClient.StartReceiving(Up, Eror);
            Console.WriteLine("Бот запущен");
            Console.ReadLine();
        }

        static async Task Up(ITelegramBotClient Botclient, Update update, CancellationToken token)
        {
            if (update.Message != null)
            {
                if(update.Message.Text == "/start")
                    StartMessage(Botclient, update);
                else
                    ForwardMessages(Botclient, update);
            } 
            else if (update.CallbackQuery != null)
            {
                if (update.CallbackQuery.Data == "Buy")
                    BuyMess(Botclient, update);
                else if (update.CallbackQuery.Data == "Write")
                    WriteMess(Botclient, update);
                else if(update.CallbackQuery.Data == "I_paid")
                    IpaindMess(Botclient, update);
            }
        }

        static async void IpaindMess(ITelegramBotClient Botclient, Update update)
        {
            string Text = "*Отправьте скриншот оплаты в бота\n\n❗ все скриншоты проверяются вручную ❗*";
            try
            {
                await Botclient.SendTextMessageAsync(update.CallbackQuery.From.Id, Text, parseMode: Telegram.Bot.Types.Enums.ParseMode.MarkdownV2);
                await Botclient.AnswerCallbackQueryAsync(update.CallbackQuery.Id);
            }
            catch { }
        }

        static async void ForwardMessages(ITelegramBotClient Botclient, Update update)
        {
            try
            {
                await Botclient.ForwardMessageAsync(ID_own, update.Message.From.Id, update.Message.MessageId);
            }
            catch { }
        }

        static async void WriteMess(ITelegramBotClient Botclient, Update update)
        {
            string Text = "💬 *Отправьте сообщение которое вы бы хотели отправить авторше и она его увидит*";
            try
            {
                await Botclient.SendTextMessageAsync(update.CallbackQuery.From.Id, Text, parseMode: Telegram.Bot.Types.Enums.ParseMode.MarkdownV2);
                await Botclient.AnswerCallbackQueryAsync(update.CallbackQuery.Id);
            }
            catch { }
        }

        static async void BuyMess(ITelegramBotClient Botclient, Update update)
        {
            string Text = "*ЦЕНА ПРИВАТНОГО КАНАЛА\\: ~250~ 200 рублей*\n\n*💳 СПОСОБЫ ОПЛАТЫ\\:*\n" +
                "*1\\. Карта Юmoney*\nНомер карты: `5599002049636634`\nНомер кошелька: `4100118439332071`\n\n*2\\. Криптовалютa\nКошелек USDT*: " +
                "\n`UQDsBnCvlDT-c9c7Al5kQ4BjTXwCQmNk4Y8WXLfrNj-SsgGg`\n\\(2 USDT\\)";

            var keyboard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Я оплатил ✅", "I_paid"),
                    },
                });
            try
            {
                await Botclient.SendTextMessageAsync(update.CallbackQuery.From.Id, Text, parseMode: Telegram.Bot.Types.Enums.ParseMode.MarkdownV2,replyMarkup: keyboard);
                await Botclient.AnswerCallbackQueryAsync(update.CallbackQuery.Id);
                if (update.CallbackQuery.From.Username != null)
                {
                    await Botclient.SendTextMessageAsync(ID_own, update.CallbackQuery.From.Username + " заинтересовался покупкой", 
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.MarkdownV2);
                }
                else
                {
                    await Botclient.SendTextMessageAsync(ID_own, update.CallbackQuery.From.Id + " заинтересовался покупкой",
                      parseMode: Telegram.Bot.Types.Enums.ParseMode.MarkdownV2);
                }
            }
            catch { }
        }

        static async void StartMessage(ITelegramBotClient Botclient, Update update)
        {
            string Text = "👋 *Привет\\, через данного бота можно приобрести доступ к приватному каналу или связаться" +
                " с авторшей\n\nВыберете что вам нужно*";

            var keyboard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Купить 💵", "Buy"),
                        InlineKeyboardButton.WithCallbackData("Написать 💬", "Write"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Посмотреть отзывы 📖", "https://t.me/+m34Af_ZAXyhmNjU6"),
                    },
                });

            try
            {
               await Botclient.SendTextMessageAsync(update.Message.From.Id, Text, parseMode: Telegram.Bot.Types.Enums.ParseMode.MarkdownV2, replyMarkup: keyboard);
            }
            catch { }
        }

        static private Task Eror(ITelegramBotClient clietn, Exception arg2, CancellationToken arg3)
        {
            return Task.CompletedTask;
        }
    }
}
