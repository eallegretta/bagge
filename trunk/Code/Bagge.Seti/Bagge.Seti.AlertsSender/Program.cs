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
using System.Net;
using System.Net.Mail;
using System.IO;
using Bagge.Seti.DataAccess.ActiveRecord;
using System.Configuration;
using System.Globalization;
using Bagge.Seti.DataAccess;

namespace Bagge.Seti.AlertsSender
{
	public class Program
	{
		private bool IsUserInteractive
		{
			get
			{
				var args = Environment.GetCommandLineArgs();
				if (args.Length == 2)
					return args[1] == "-u";
				return false;
			}
		}

		static void Main(string[] args)
		{
			new Program().Run();
		}

		public void Run()
		{
			Console.Clear();
			try
			{
				InitializeCulture();
				Initialize();

				using (new SessionScope(FlushAction.Never))
				{
					IoCContainer.TicketManager.SendEmails = false;
					SendAlertsByTicketExpired();
					SendAlertsByBudgetExpired();
				}
			}
			catch (Exception ex)
			{
				PrintException(ex);
			}

			if (IsUserInteractive)
			{
				Console.WriteLine(new string('-', 78));
				Console.WriteLine(new string('-', 78));
				Console.WriteLine("Presione un tecla para finalizar");
				Console.ReadKey();
			}
		}

		protected void PrintException(Exception ex)
		{
			while (ex != null)
			{
				Console.WriteLine();
				Console.WriteLine(new string('-', 78));
				Console.WriteLine(new string('-', 78));
				Console.WriteLine("Error: ");
				Console.WriteLine("\t" + ex.Message);
				Console.WriteLine();
				Console.WriteLine("Stack Trace: ");
				Console.WriteLine("\t" + ex.StackTrace);
				Console.WriteLine();
				Console.WriteLine("Source: ");
				Console.WriteLine("\t" + ex.Source);
				ex = ex.InnerException;
			}
		}

		protected void InitializeCulture()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo(ConfigurationManager.AppSettings["culture"]);
		}

		protected void Initialize()
		{
			IConfigurationSource config = ActiveRecordSectionHandler.Instance;
			Assembly asm = Assembly.Load("Bagge.Seti.BusinessEntities");
			ActiveRecordStarter.Initialize(asm, config);

			IoCContainer.User = Thread.CurrentPrincipal;
		}

		protected void SendAlertsByTicketExpired()
		{
			Console.Write("Buscando todos los tickets abiertos:");
			Ticket[] tickets = IoCContainer.TicketManager.FindAllByStatus(TicketStatusEnum.Open);
			Console.WriteLine(".......................Encontrados: " + tickets.Length);
			if (tickets.Length > 0)
			{
				Console.WriteLine("\tObteniendo configuration de alertas");
				AlertConfiguration alert = IoCContainer.AlertConfigurationManager.Get();
				Console.WriteLine("\tProcesando tickets");
				for (int index = 0; index < tickets.Length; index++)
				{
					if (tickets[index].ExecutionDateTime < DateTime.Now)
					{
						int id = tickets[index].Id;

						var ticket = IoCContainer.TicketManager.Get(id);
						TicketStatus ticketStatus = IoCContainer.TicketStatusManager.Get(TicketStatusEnum.Expired);
						ticket.Status = ticketStatus;

						IoCContainer.TicketManager.Update(ticket);

						Console.WriteLine("\tTicket #{0} marcado como Expirado - Enviando Email", id);

						SendMail(alert.SendEmailToCreator, alert.SendEmailToEmployees, ticket, "Alerta: Vencimiento de Ticket", "Se vencio el Ticket: ");
					}
				}
			}
		}

		protected void SendAlertsByBudgetExpired()
		{
			Console.Write("Buscando todos los tickets pendientes de aprobacion:");
			Ticket[] tickets = IoCContainer.TicketManager.FindAllByStatus(TicketStatusEnum.PendingAproval);
			Console.WriteLine(".......Encontrados: " + tickets.Length);
			if (tickets.Length > 0)
			{
				Console.WriteLine("\tObteniendo configuration de alertas");
				AlertConfiguration alert = IoCContainer.AlertConfigurationManager.Get();
				Console.WriteLine("\tProcesando tickets");
				for (int index = 0; index < tickets.Length; index++)
				{
                    if ((tickets[index].CreationDate).AddDays(alert.MaxDaysPendingAproval) < DateTime.Now)
					{
						var id = tickets[index].Id;
						var ticket = IoCContainer.TicketManager.Get(id);
						TicketStatus ticketStatus = IoCContainer.TicketStatusManager.Get(TicketStatusEnum.Canceled);
						ticket.Status = ticketStatus;

						IoCContainer.TicketManager.Update(ticket);

						Console.WriteLine("\tTicket #{0} marcado como Cancelado - Enviando Email", id);
						SendMail(alert.SendEmailToCreator, alert.SendEmailToEmployees, ticket, "Alerta: Cancelacion de Ticket", "Se cancelo el Ticket: ");
					    
                    }
				}
			}
		}

		protected void SendMail(bool isSendEmailToCreator, bool isSendEmailToEmployees, Ticket ticket, string subjectEmail, string bodyEmail)
		{
			using (var msg = new MailMessage())
			{

				if (isSendEmailToCreator)
				{
					if (!string.IsNullOrEmpty(ticket.Creator.Email))
						msg.To.Add(new MailAddress(ticket.Creator.Email));
				}

				if (isSendEmailToEmployees)
				{
					foreach (var employee in ticket.Employees)
					{
						if (!string.IsNullOrEmpty(employee.Email))
							msg.To.Add(employee.Email);
					}
				}

				if (msg.To.Count == 0)
					return;

				msg.Subject = subjectEmail;

				string applicationPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
				string body = File.ReadAllText(applicationPath + @"\mailTemplate.htm");

				body = body.Replace("{BODY_MAIL_SUBJECT}", bodyEmail);
				body = body.Replace("{ID}", ticket.Id.ToString());
				body = body.Replace("{CUSTOMER_NAME}", ticket.Customer.Name);
				body = body.Replace("{DESCRIPTION}", ticket.Description);
				body = body.Replace("{CREATION_DATE}", string.Format("{0:d}", ticket.CreationDate));
				body = body.Replace("{EXECUTION_DATE}", string.Format("{0:d}", ticket.ExecutionDateTime));
				body = body.Replace("{ESTIMATED_DURATION}", string.Format("{0:#.##} hs", ticket.EstimatedDuration));
				body = body.Replace("{STATUS}", ticket.Status.Name);

				msg.Body = body;

				msg.IsBodyHtml = true;

				SmtpClient smtpClient = new SmtpClient();
				smtpClient.EnableSsl = true;

				if (isSendEmailToCreator || isSendEmailToEmployees)
					smtpClient.Send(msg);
			}
		}
	}
}