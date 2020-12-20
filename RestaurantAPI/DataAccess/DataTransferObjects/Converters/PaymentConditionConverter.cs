using System;
using DataAccess.Models;
using System.Linq;

namespace DataAccess.DataTransferObjects.Converters
{
    public partial class Converter
    {
        public static PaymentConditionDTO Convert(PaymentCondition obj)
        {
            return new PaymentConditionDTO(obj.Condition)
            {
                Id = obj.Id,
                //Condition = obj.Condition
            };
        }

        public static PaymentCondition Convert(PaymentConditionDTO obj)
        {
            return new PaymentCondition
            {
                Id = obj.Id,
                Condition = obj.Condition
            };
        }

        //public static PaymentCondition Convert(PaymentConditionDTO obj)
        //{
        //    PaymentConditionDTO d = obj.Id.
        //    return new PaymentCondition
        //    {
        //        Id = Id,
        //        Condition = obj
        //    };
        //}
    }
}