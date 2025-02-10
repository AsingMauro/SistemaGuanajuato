using MailKit.Net.Smtp;
using MimeKit;

namespace SistemaGuanajuato.Services
{
    public class CorreoService
    {
        private readonly string smtpServer = "smtp.gmail.com";
        private readonly int smtpPort = 587; // También puedes usar 465 para SSL
        private readonly string correoOrigen = "sistemaguanajuato2025@gmail.com"; // Reemplaza con tu correo
        private readonly string contraseña = "kxpn vsud camb ykax"; // Usa una contraseña de aplicación si tienes 2FA

        public async Task<(bool, int)> EnviarCorreoAsync(string destinatario, string nombreDestinatario)
        {
            Random rand = new Random();
            int numeroCodigo = rand.Next(1000, 9999);
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Sistema Guanajuato", correoOrigen));
                emailMessage.To.Add(new MailboxAddress(destinatario, destinatario));
                emailMessage.Subject = "Codigo de validacion";

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = "<!DOCTYPE html><html lang='es'><head><meta charset='UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'>" +
                    "<title>Código de Verificación</title></head><body style='font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0;'>" +
                    "<table role='presentation' style='width: 100%; max-width: 600px; margin: 20px auto; background-color: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);'>" +
                    "<tr><td style='text-align: center; padding: 10px 0;'><h2 style='color: #333;'>🔐 Verificación de Cuenta</h2></td></tr><tr>" +
                    "<td style='text-align: center; padding: 10px;'><p style='color: #555; font-size: 16px;'>Hola, "+nombreDestinatario+"</p><p style='color: #555; font-size: 16px;'>" +
                    "Tu código de verificación es: </p><div style='background-color: #007bff; color: #ffffff; font-size: 24px; font-weight: bold; padding: 10px; margin: 10px auto; display: inline-block; border-radius: 5px;'>" +
                    "<span>"+ numeroCodigo + "</span></div><p style='color: #555; font-size: 14px;'>Usa este código para completar tu registro. Si no solicitaste este código, ignora este mensaje." +
                    "</p></td></tr><tr><td style='text-align: center; padding-top: 20px;'><p style='font-size: 12px; color: #777;'>© 2025 - Guanajuato | Todos los derechos reservados.</p></td></tr></table></body></html>"
                };

                emailMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(correoOrigen, contraseña);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }

                return (true, numeroCodigo);
            }
            catch
            {
                return (false, -1);
            }
        }
    }

}

