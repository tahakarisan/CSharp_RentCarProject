using Castle.DynamicProxy;
using CoreLayer.Utilities.Interceptors;
using System.Transactions;

namespace CoreLayer.Aspects.Autofac
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope _transaction = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    _transaction.Complete();
                }
                catch (System.Exception)
                {
                    _transaction.Dispose();
                    throw;
                }
            }
        }

    }
}
