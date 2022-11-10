namespace Core.UseCases.Vouchers.Queries.GetVouchers;

public class GetPromocodesByVaucherIdQueryOutDto
{
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsActivated { get; set; }
        public int DistanceId { get; set; }
}