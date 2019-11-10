using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Text.RegularExpressions;
using Tcc.Apoio;
using Tcc.Models;

namespace Tcc.Entity
{
    public class EnviarSMSSenha : TaskActivity
    {
        static AmazonSimpleNotificationServiceClient snsClient = new AmazonSimpleNotificationServiceClient("AKIA35OHI44PFEM6VANA", "HqV0YXVuBmkJyH7C5gcT/EPj8dJRC/0PPg+FANIx", Amazon.RegionEndpoint.USEast1);

        public EnviarSMSSenha(GenClass prContextoExecucao)
            : base(prContextoExecucao)
        {
        }

        private User aUser;
        private ApplicationUser aApplicationUser;
        private ApplicationUserManager aApplicationUserManager;

        protected override bool PreCondicional()
        {
            aUser = new UsersRepository().GetUserId(aApplicationUser.Id);

            if (aUser == null)
            {
                return aContextoExecucao.withoutError(newError("usuario nao encontrado"));
            }

            if (string.IsNullOrWhiteSpace(aUser.telefone))
            {
                aContextoExecucao.addErro("Usuario nao possui telefone cadastrado.");
            }

            return aContextoExecucao.withoutError();
        }

        protected override bool Semantic()
        {
            Topic lTopic = new Topic();
            lTopic.TopicArn = "arn:aws:sns:sa-east-1:819146057502:forgotPassword";

            string lGuid = Guid.NewGuid().ToString();

            string lSenha = "!" + lGuid.Substring(0, 5).ToLower() + "$" + lGuid.Substring(20, 3).ToUpper();

            Regex r = new Regex("[a-z]");
            if (!r.Match(lSenha).Success)
                lSenha += "e";

            r = new Regex("[A-Z]");
            if (!r.Match(lSenha).Success)
                lSenha += "F";

            string token = aApplicationUserManager.GeneratePasswordResetToken(aUser.membershipid);
            IdentityResult result = aApplicationUserManager.ResetPassword(aUser.membershipid, token, lSenha);

            if (result.Succeeded)
            {
                PublishRequest pubRequest = new PublishRequest();
                pubRequest.Message = string.Format("Olá, {0}\nPor favor, logar com a senha a seguir e trocá-la: {1}", aUser.nome, lSenha);
                pubRequest.PhoneNumber = "+55" + aUser.telefone.Trim().removerCaracteresEspeciais();
                PublishResponse pubResponse = snsClient.Publish(pubRequest);
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    aContextoExecucao.addErro(item);
                }
            }

            return aContextoExecucao.withoutError();
        }

        public bool enviarSMS(ApplicationUser prUser, ApplicationUserManager applicationUserManager)
        {
            aApplicationUser = prUser;
            aApplicationUserManager = applicationUserManager;

            execute();

            return aContextoExecucao.withoutError();
        }
    }
}