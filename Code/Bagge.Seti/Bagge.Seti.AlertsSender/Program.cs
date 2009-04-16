using System;
using System.Collections.Generic;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessLogic;
using Castle.ActiveRecord.Framework;
using System.Reflection;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using Bagge.Seti.Common;
using System.Threading;

namespace Bagge.Seti.AlertsSender
{
	public class Program
	{
		static void Main(string[] args)
		{
			Initialize();

			Console.WriteLine("Hello World!!!");
			//SendAlertsByTicketExpired();
			//SendlertsByBudgetExpired();
		}

		private static void Initialize()
		{
			IConfigurationSource config = ActiveRecordSectionHandler.Instance;
			Assembly asm = Assembly.Load("Bagge.Seti.BusinessEntities");
			ActiveRecordStarter.Initialize(asm, config);

			IoCContainer.User = Thread.CurrentPrincipal;
		}

		//public static void SendAlertsByTicketExpired()
		//{
		//    TicketManager ticketManager = new TicketManager();
		//    Ticket[] tickets = ticketManager.FindAllByStatus("Status", "Abierto");

		//    AlertConfigurationManager alertConfigurationManager = new AlertConfigurationManager();
		//    AlertConfiguration alert = alert.GetActualAlert();

		//    for (int i = 0; i < tickets.Length; i++)
		//    {
		//        if (tickets[i].ExecutionDate > DateTime.Now)
		//        {
		//            Ticket ticket = new Ticket();
		//            ticket = ticketManager.Get(tickets[i].Id);
		//            ticket.Status = "Vencido";
		//            ticketManager.Update(ticket);

		//            SendMailByExpired(ticket, "Ticket");
		//        }
		//    }
		//}
		//public static void SendAlertsByBudgetExpired()
		//{
		//    TicketManager ticketManager = new TicketManager();
		//    Ticket[] tickets = ticketManager.FindAllByStatus("Status", "Pendientes de Aprobacion");
            
		//    AlertConfigurationManager alertConfigurationManager = new AlertConfigurationManager();
		//    AlertConfiguration alert = alert.GetActualAlert();

		//     for (int i = 0; i < tickets.Length; i++) 
		//     {
		//         if (tickets[i].CreationDate > tickets[i].CreationDate + alert.Days)
		//         {
		//             Ticket ticket = new Ticket();
		//             ticket = ticketManager.Get(tickets[i].Id);
		//             ticket.Status = "Cancelado";
		//             ticketManager.Update(ticket);

		//             SendMailByExpired(ticket, "Budget");
		//         }
		//     }
		//}
		//public static void SendMailByExpired(string expirationConcept)
		//{ 
        
		//}
	}
}