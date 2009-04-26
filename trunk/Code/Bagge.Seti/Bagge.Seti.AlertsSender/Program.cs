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
            AlertConfiguration alert = IoCContainer.AlertConfigurationManager.Get();

            for (int index=0; index<tickets.Length; index++)
            {
                if (tickets[index].ExecutionDate < DateTime.Now)
                {
                    Ticket ticket = new Ticket();
                    ticket = IoCContainer.TicketManager.Get(tickets[index].Id);
                    TicketStatus ticketStatus = IoCContainer.TicketStatusManager.Get(TicketStatusEnum.Expired);
                    ticket.Status = ticketStatus;

                    IoCContainer.TicketManager.Update(ticket);

                    SendMail(true, alert.SendEmailToCreator, alert.SendEmailToEmployees, ticket, "Alerta: Vencimiento de Ticket", "Se vencio el Ticket: ");
                }
            }
        }

        public static void SendAlertsByBudgetExpired()
        {
            Ticket[] tickets = IoCContainer.TicketManager.FindAllByStatus(TicketStatusEnum.PendingAproval);
            AlertConfiguration alert = IoCContainer.AlertConfigurationManager.Get();
            
            for (int index=0; index<tickets.Length; index++)
            {
                if ((tickets[index].CreationDate).AddDays(alert.Days) > DateTime.Now)
                {
                    Ticket ticket = new Ticket();
                    ticket = IoCContainer.TicketManager.Get(tickets[index].Id);
                    TicketStatus ticketStatus = IoCContainer.TicketStatusManager.Get(TicketStatusEnum.Canceled);
                    ticket.Status = ticketStatus;

                    IoCContainer.TicketManager.Update(ticket);

                    SendMail(false, alert.SendEmailToCreator, alert.SendEmailToEmployees, ticket, "Alerta: Cancelacion de Ticket", "Se cancelo el Ticket: ");
                }
            }
        }

        public static void SendMail(bool toAll, bool isSendEmailToCreator, bool isSendEmailToEmployees, Ticket ticket, string subjectEmail, string bodyEmail)
        {
            MailMessage msg = new MailMessage();
          
            if (isSendEmailToCreator == true)
            {
                msg.To.Add(new MailAddress(ticket.Creator.Email.ToString()));
            }

            if ((toAll == true) && (isSendEmailToEmployees == true))
            {
                List<Employee> lstEmployees = (List<Employee>)((object)ticket.Employees);

                for (int index=0; index<lstEmployees.Count; index++)
                {
                    msg.To.Add(new MailAddress(lstEmployees[index].Email.ToString()));
                }   
            }

            msg.Subject = subjectEmail;
            
            string ApplicationPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string[] strsMailTemplate = File.ReadAllLines(ApplicationPath + @"\mailTemplate.htm");
            
            string strMailTemplate = strsMailTemplate.ToString();

            strMailTemplate.Replace("{BODY_MAIL_SUBJECT}", bodyEmail);
            strMailTemplate.Replace("{ID}", ticket.Id.ToString());
            strMailTemplate.Replace("{CUSTOMER_NAME}", ticket.Customer.Name.ToString());
            strMailTemplate.Replace("{DESCRIPTION}", ticket.Description.ToString());
            strMailTemplate.Replace("{CREATION_DATE}", ticket.CreationDate.ToString());
            strMailTemplate.Replace("{EXECUTION_DATE}", ticket.ExecutionDate.ToString());
            strMailTemplate.Replace("{ESTIMATED_DURATION}", ticket.EstimatedDuration.ToString());
            strMailTemplate.Replace("{STATUS}", ticket.Status.Description.ToString());

            msg.Body = strMailTemplate;

            msg.IsBodyHtml = true;
            
            SmtpClient clienteSmtp = new SmtpClient();

            try
            {
                if ((isSendEmailToCreator == true) || (isSendEmailToEmployees == true))
                {
                    clienteSmtp.Send(msg);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            msg.Dispose();
        }
	}
}