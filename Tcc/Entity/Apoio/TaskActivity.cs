using System.Transactions;

namespace Tcc.Apoio
{
    public class TaskActivity : GenClass
    {
        public TaskActivity()
        {
            
        }

        protected virtual bool PreCondicional()
        {
            return withoutError();
        }

        protected virtual bool Semantic()
        {
            return withoutError();
        }

        protected bool execute()
        {
            using (TransactionScope _scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    if (PreCondicional())
                    {
                        if (Semantic())
                        {
                            _scope.Complete();
                            return true;
                        }
                    }
                }
                catch (System.Exception e)
                {
                    addErro("Ocorreu um erro inesperado.");                    
                }

                _scope.Dispose();
                return false;
            }
        }
    }
}