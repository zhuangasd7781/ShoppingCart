using DataBases;
using Models;
using Service;
using Serilog;

namespace Business
{
    public class accountBusiness
    {
        accountService _accountService;
        public accountBusiness(IDB _db)
        {
            _accountService = new accountService(_db);
        }
        public async Task<account> Get(string acc)
        {
            try
            {
                return await _accountService.Get(acc);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public async Task<uint> Insert(account acc)
        {
            try
            {
                account dm = await Get(acc.id);
                if (dm.pk > 0)
                    throw new Exception("此帳號已經存在");
                acc.pwd = acc.pwd.MD5();
                acc.date = DateTime.Now.Date("yyyy-MM-dd HH:mm:ss");
                acc.role = 0;
                acc.status = "V";
                return await _accountService.Insert(acc);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
