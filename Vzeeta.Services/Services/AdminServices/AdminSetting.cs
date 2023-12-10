using Vzeeta.Core.Model;
using Vzeeta.Core.Repository;
using Vzeeta.Core.ViewModels;
using Vzeeta.Services.Interfaces.IAdmin;

namespace Vzeeta.Services.Services.AdminServices
{
    public class AdminSetting : IAdminSetting
    {
        IRepository<DiscountCode_Coupon, int> _couponRepository;
        private readonly IRepository<Booking, int> usesDiscountCodes;

        public AdminSetting(IRepository<DiscountCode_Coupon, int> _couponRepository, IRepository<Booking, int> usesDiscountCodes)
        {
            this._couponRepository = _couponRepository;
            this.usesDiscountCodes = usesDiscountCodes;
        }

        public async Task<bool> Add(DiscountCodeVM entity)
        {
            DiscountCode_Coupon newCode = new DiscountCode_Coupon
            {
                Code = entity.Code,
                DiscountType = entity.DiscountType,
                NoOfRequests = entity.NoOfRequests,
                Value = entity.Value
            };
            var result = await _couponRepository.Add(newCode);
            if (result)
                return true;
            else
                return false;
        }
        public async Task<bool> Deactivate(int Id)
        {
            var result = await _couponRepository.GetById(Id);
            if (result != null)
            {
                result.IsExpired = true;
                var deactivationDone = await _couponRepository.Update(result);
                if (deactivationDone)
                    return true;
                else
                    throw new Exception($"Already Deactivated Code! IsExpired: {result.IsExpired}");
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _couponRepository.Delete(id);
            return result;
        }

        public async Task<bool> Update(DiscountCode_Coupon entity)
        {
            var found = await _couponRepository.GetById(entity.ID);
            if (found != null)
            {
                var IsUsedDiscountCode = usesDiscountCodes.GetAllEntities().Any(x => x.DiscountCode == found.Code);
                if (!IsUsedDiscountCode)
                {
                    found.Code = entity.Code;
                    found.NoOfRequests = entity.NoOfRequests;
                    found.DiscountType = entity.DiscountType;
                    found.IsExpired = entity.IsExpired;
                    found.Value = entity.Value;
                    var result = await _couponRepository.Update(found);
                    return result;

                }
                else
                {
                    throw new Exception("Used DiscountCode");
                }
            }
            return false;
        }
    }
}
