using System;
using DataAccess.Models;

namespace DataAccess.DataTransferObjects
{
    public enum PaymentConditions
    {
        Bestilt,
        Begyndt,
        Leveret,
        Betalt,
        Annulleret
    }

    public class PaymentConditionDTO
    {
        private int _id;

        public PaymentConditionDTO(string condition)
        {
            Condition = condition;
        }

        public int Id
        {
            get
            {
                var result = Enum.TryParse(Condition, out PaymentConditions nukeStatus);

                switch ((int) nukeStatus)
                {
                    case 0:
                        _id = (int) nukeStatus + 1;
                        return _id;
                    case 1:
                        _id = (int) nukeStatus + 1;
                        return _id;
                    case 2:
                        _id = (int) nukeStatus + 1;
                        return _id;
                    case 3:
                        _id = (int) nukeStatus + 1;
                        return _id;
                    case 4:
                        _id = (int) nukeStatus + 1;
                        return _id;
                    default:
                        _id = 0;
                        return _id;
                }
            }
            set => _id = value;
        }

        public string Condition { get; set; }
        public PaymentCondition PayCondition { get; set; }
    }

    public static class PaymentConditionDescriptor
    {
        public static string Describe(PaymentConditions type)
        {
            return type switch
            {
                PaymentConditions.Bestilt => "Bestilt",
                PaymentConditions.Begyndt => "Begyndt",
                PaymentConditions.Leveret => "Leveret",
                PaymentConditions.Betalt => "Betalt",
                PaymentConditions.Annulleret => "Annulleret",
                _ => null
            };
        }

        public static PaymentConditions Parse(string PaymentConditionsText)
        {
            return PaymentConditionsText switch
            {
                "Bestilt" => PaymentConditions.Bestilt,
                "Begyndt" => PaymentConditions.Begyndt,
                "Leveret" => PaymentConditions.Leveret,
                "Betalt" => PaymentConditions.Betalt,
                "Annulleret" => PaymentConditions.Annulleret,
                _ => PaymentConditions.Annulleret
            };
        }
    }
}