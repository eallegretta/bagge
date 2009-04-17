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

namespace Bagge.Seti.AlertsSender
{
	public class Program
	{
		static void Main(string[] args)
		{
			Initialize();
			SendAlertsByTicketExpired();
			SendAlertsByBudgetExpired();
		}

		private static void Initialize()
		{
			IConfigurationSource config = ActiveRecordSectionHandler.Instance;
			Assembly asm = Assembly.Load("Bagge.Seti.BusinessEntities");
			ActiveRecordStarter.Initialize(asm, config);

			IoCContainer.User = Thread.CurrentPrincipal;
		}

		public static void SendAlertsByTicketExpired()
        {
            Ticket[] tickets = IoCContainer.TicketManager.FindAllByStatus(TicketStatusEnum.Open);
            //AlertConfiguration alert = IoCContainer.AlertConfigurationManager.GetActualAlert();

            for (int index=0; index<tickets.Length; index++)
            {
                if (tickets[index].ExecutionDate < DateTime.Now)
                {
                    Ticket ticket = new Ticket();
                    ticket = IoCContainer.TicketManager.Get(tickets[index].Id);
                    //ticket.Status = TicketStatusEnum.Expired;
                    IoCContainer.TicketManager.Update(ticket);

                    SendMail(true, ticket, "Alerta: Vencimiento de Ticket", "Se vencio el Ticket: ");
                }
            }
        }

        public static void SendAlertsByBudgetExpired()
        {
            Ticket[] tickets = IoCContainer.TicketManager.FindAllByStatus(TicketStatusEnum.PendingAproval);
            //AlertConfiguration alert = IoCContainer.AlertConfigurationManager.GetActualAlert();
            for (int index=0; index<tickets.Length; index++)
            {
                //'Clave' un parametro para que compile temporalmente, ya que va 'alert.Days'
                if ((tickets[index].CreationDate).AddDays(10) > DateTime.Now)
                {
                    Ticket ticket = new Ticket();
                    ticket = IoCContainer.TicketManager.Get(tickets[index].Id);
                    //ticket.Status = TicketStatusEnum.Canceled;
                    IoCContainer.TicketManager.Update(ticket);

                    SendMail(false, ticket, "Alerta: Cancelacion de Ticket", "Se cancelo el Ticket: ");
                }
            }
        }

        public static void SendMail(bool toAll, Ticket ticket, string subjectEmail, string bodyEmail)
        {
            MailMessage msg = new MailMessage();

            msg.To.Add(new MailAddress(ticket.Creator.Email.ToString()));

            if (toAll == true)
            {
                List<Employee> lstEmployees = (List<Employee>)((object)ticket.Employees);

                for (int index=0; index<lstEmployees.Count; index++)
                {
                    msg.To.Add(new MailAddress(lstEmployees[index].Email.ToString()));
                }   
            }

            msg.From = new MailAddress("admin@seti.com");
            msg.Subject = subjectEmail;

            msg.Body = bodyEmail + "\n";
            msg.Body = msg.Body + ticket.Id.ToString() + "\n";
            msg.Body = msg.Body + ticket.Customer.ToString() + "\n";
            msg.Body = msg.Body + ticket.Description.ToString() + "\n";
            msg.Body = msg.Body + ticket.CreationDate.ToString() + "\n";
            msg.Body = msg.Body + ticket.ExecutionDate.ToString() + "\n";
            msg.Body = msg.Body + ticket.EstimatedDuration.ToString() + "\n";

            SmtpClient clienteSmtp = new SmtpClient("smtp.seti.com");

            clienteSmtp.Credentials = new NetworkCredential("admin", "1234");

            try
            {
                clienteSmtp.Send(msg);
            }

            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.ReadLine();
            }
        }
	}
}