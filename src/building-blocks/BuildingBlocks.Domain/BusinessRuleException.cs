﻿namespace BuildingBlocks.Domain
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message)
        {
        }
    }
}
