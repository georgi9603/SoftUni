using _03BarracksFactory.Contracts;
using System;

namespace P03_BarraksWars.Core.Command
{
    public class RetireCommand : Command
    {
        public RetireCommand(string[] data, IRepository repository, IUnitFactory unitFactory) 
            : base(data, repository, unitFactory)
        {
        }

        public override string Execute()
        {
            try
            {
                this.Repository.RemoveUnit(this.Data[1]);
                return $"{Data[1]} retired!";
            }
            catch (Exception e)
            {
                return  e.Message;
            }
        }
    }
}
