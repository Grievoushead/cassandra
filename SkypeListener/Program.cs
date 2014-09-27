using System;
using DI;
using Service;
using SKYPE4COMLib;

namespace SkypeListener
{
    class Program
    {
        private const string SEPARATOR = "///////////////////////////////////////////////";

        private static readonly Skype skype = new Skype();

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            IoC.Initialize(new UnityDependencyResolver());
            ((_ISkypeEvents_Event)skype).AttachmentStatus += status =>
            {
                if (status == TAttachmentStatus.apiAttachAvailable)
                {
                    Console.WriteLine("Skype attaching...");
                    skype.Attach(Wait: false);
                }
                if (status == TAttachmentStatus.apiAttachSuccess)
                {
                    Console.WriteLine("Skype Listener attached");
                    Console.WriteLine("Comand Logs: ");
                    Console.WriteLine(SEPARATOR.Replace('/', '-'));
                }
            };

            skype.Attach(Wait: false);

            if (!skype.Client.IsRunning)
            {
                Console.WriteLine("Skype client running...");
                skype.Client.Start();
                Console.WriteLine("Skype client started");
            }

            skype.MessageStatus += (message, status) =>
            {
                if (status != TChatMessageStatus.cmsReceived)
                {
                    return;
                }

                Console.WriteLine("Msg received from: [" + message.FromDisplayName + "]");
                Console.WriteLine("Msg received with status: [" + status + "]");
                Console.WriteLine("Msg: [" + message.Body + "]");
                Console.WriteLine(SEPARATOR);
                Console.WriteLine(Environment.NewLine);

                // Eval cmd
                message.Chat.SendMessage(new CommandContext().Execute(message.Body).Result);
            };

            while (true)
            {
                // skype listener ready
            }
        }
    }
}
