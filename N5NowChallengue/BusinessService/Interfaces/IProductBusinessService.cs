using N5NowChallengue.BusinessService.DTO;

namespace N5NowChallengue.BusinessService.Interfaces
{
    public interface IProductBusinessService
    {
        MessageDto AddProduct(NewProductDto newProductDto);
    }
}