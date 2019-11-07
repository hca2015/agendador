using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNet.Identity;
using System;
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
                return aContextoExecucao.withoutError(newError("usuario nao encontrado"));

            if (string.IsNullOrWhiteSpace(aUser.telefone))
                aContextoExecucao.addErro("Usuario nao possui telefone cadastrado.");

            return aContextoExecucao.withoutError();
        }

        protected override bool Semantic()
        {
            Topic lTopic = new Topic();
            lTopic.TopicArn = "arn:aws:sns:sa-east-1:819146057502:forgotPassword";

            string lGuid = Guid.NewGuid().ToString();

            string lSenha = "!" + lGuid.Substring(0, 5) + "$" + lGuid.Substring(20, 4);

            var token = aApplicationUserManager.GeneratePasswordResetToken(aUser.membershipid);
            var result = aApplicationUserManager.ResetPassword(aUser.membershipid, token, lSenha);
                        
            PublishRequest pubRequest = new PublishRequest();
            pubRequest.Message = string.Format("Olá, {0}\nPor favor, logar com a senha a seguir e trocá-la: {1}", aUser.nome, lSenha);
            pubRequest.PhoneNumber = "+55" + aUser.telefone.Trim().removerCaracteresEspeciais();
            PublishResponse pubResponse = snsClient.Publish(pubRequest);

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