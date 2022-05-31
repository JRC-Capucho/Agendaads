using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;

namespace AgendaADS;

public partial class Form1 : Form
{
    private TelegramBotClient tBot;
    private String idChat = "";
    private Db bancoDados = new Db();
    private System.Timers.Timer aTimer;

    public Form1()
    {
        InitializeComponent();
    }

    [Obsolete]
    private void Form_Load(object sender, EventArgs e)
    {
        tBot = new TelegramBotClient(idChat);
        tBot.OnMessage += tBot_OnMessage;
        tBot.OnCallbackQuery += tBot_OnCallbackQuery;
        tBot.StartReceiving();
        tBot.StopReceiving();
    }

    [Obsolete]
    private void btStart_Click(object sender, EventArgs e)
    {
        tBot.StartReceiving();
    }

    [Obsolete]
    private void btStop_Click(object sender, EventArgs e)
    {
        bancoDados.fecharDB();
        tBot.StopReceiving();
    }

    private void tBot_OnCallbackQuery(object? sender, CallbackQueryEventArgs e)
    {
        var id = e.CallbackQuery.From.Id;
        var msg = e.CallbackQuery.Data.ToString();
        string aux,aux1;
        
        msg = msg.ToLower();

            if(msg == "sim")
            {
                tBot.EditMessageTextAsync(id,e.CallbackQuery.Message.MessageId,
                "\n<b>Agenda Fatec 3 ADS</b>\n"
                +
                "\nVocê será notificado quando faltar 10 minutos da sua proxima aula!",
                Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: null);

                    Boolean sair = true;
                do{

                    if(bancoDados.inicioDaAula() != null)
                    {
                        aux1 = bancoDados.inicioDaAula();
                        aux = bancoDados.msgAula(aux1);

                        tBot.SendTextMessageAsync(id,"\n<b>Agenda Fatec 3 ADS</b>\n" + aux + "\n",
                        Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: null);
                    }

                    Thread.Sleep(60000);

                }while(sair);
            }
            else if(msg == "nao")
            {
                tBot.EditMessageTextAsync(id,e.CallbackQuery.Message.MessageId,
                "\n<b>Agenda Fatec 3 ADS</b>\n"
                +
                "\nTchau, até a proxima!",
                Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: null);
            }
    }

    
    
    private void tBot_OnMessage(object? sender, MessageEventArgs e)
    {
        var id = e.Message.From.Id;
        var msg = e.Message.Text.ToString();
        msg = msg.ToLower();
            

            if(msg.Equals("3ads"))
            {

                tBot.SendTextMessageAsync(id,
                "\n<b>Agenda Fatec 3 ADS</b>\n" +
                "\nOla, aluno do terceiro semestre de analise e desenvolvimento de sistemas!" +
                "\nDeseja ser notificado quando quando a aula começar!",
                Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup:CreateButton());

            }
            else
            {
                tBot.SendTextMessageAsync(id,
                "\n<b>Agenda Fatec 3 ADS</b>\n" +
                "\nOi, tudo bem? deseja ser notificado quando quando a aula começar!",
                Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup:CreateButton());
            }

    }

    public InlineKeyboardMarkup CreateButton()
    {
        List<InlineKeyboardButton> btn = new List<InlineKeyboardButton>();
        btn.Add(new InlineKeyboardButton{Text="Sim",CallbackData="sim"});
        btn.Add(new InlineKeyboardButton{Text="Não",CallbackData="nao"});

        var menu = new List<InlineKeyboardButton[]>();
        menu.Add(new[] { btn[0] , btn[1] });
        var main = new InlineKeyboardMarkup(menu.ToArray());
        return main;
    }
}
