namespace Invoices.Common
{
    public static class ValidationConstants
    {
        // Product
        public const int ProductNameMaxLength = 30;
        public const int ProductPriceMinLength = 1000;

        // Address
        public const int AddressStreetNameMaxLength = 20;        
        public const int AddressCityMaxLength = 15;
        public const int AddressCountryMaxLength = 15;

        // Invoice
        public const int InvoiceNumberMaxLength = 1500000000;

        // Client
        public const int ClientNameMaxLength = 25;
        public const int ClientNumberVatMaxLength = 15;
    }
}
