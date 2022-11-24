namespace Core.UseCases.Vouchers.Commands.AddPromocodesToVoucher;
public class AddPromocodesToVoucherCommandInDto
{
    public ICollection<PromocodeDto> Promocodes { get; set; }

    public class PromocodeDto
    {
        public int DistanceId { get; set; }
        public int Quantity { get; set; }

    }
}