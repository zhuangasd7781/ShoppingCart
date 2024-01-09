using DataBases;
using Models;
using Service;
using Newtonsoft.Json;
using Serilog;
using Microsoft.AspNetCore.Http;

namespace Business
{
    public class commodityBusiness
    {
        commodityService _commodityService;
        public commodityBusiness(IDB _db)
        {
            _commodityService = new commodityService(_db);
        }
        #region
        public async Task<commodity> Get(int pk)
        {
            try
            {
                return await _commodityService.Get(pk.UInt());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> Insert(commodity dm, int AccPk)
        {
            try
            {
                dm.acc_fk = AccPk;
                uint inseriPk = await _commodityService.Insert(dm);
                return inseriPk.Int();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> Update(commodity dm)
        {
            try
            {
                return await _commodityService.Update(dm);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> Delete(int pk)
        {
            try
            {
                commodity dm = await Get(pk);
                if (!string.IsNullOrEmpty(dm.pic))
                {
                    var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "pic", dm.pic);
                    if (File.Exists(FilePath))
                    {
                        File.Delete(FilePath);
                    }
                }
                return await _commodityService.Delete(pk.UInt());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<commodity>> GetList(int accPk = 0)
        {
            try
            {
                return await _commodityService.GetList(accPk);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        #endregion
        public async Task<int> Set(string companyJson, int AccPk, IFormFile file)
        {
            try
            {
                commodity? _commodity = JsonConvert.DeserializeObject<commodity>(companyJson);
                string filepath = "";
                if (_commodity == null) throw new Exception("發生錯誤,請稍後再試\n查無此產品");
                if (file != null)
                {
                    string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    _commodity.pic = filename;
                    filepath = await Upload(file, filename);
                }
                if (await Get(_commodity.pk) == null)
                {
                    return await Insert(_commodity, AccPk);
                }
                else
                {
                    if (file != null)
                    {
                        commodity? originalCommodity = await Get(_commodity.pk);
                        if (originalCommodity == null)
                        {
                            if (File.Exists(filepath))
                                File.Delete(filepath);
                            throw new Exception("發生錯誤,請稍後再試\n查無此產品");
                        }
                        var originalFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "pic", originalCommodity.pic);
                        if (File.Exists(originalFilePath))
                        {
                            File.Delete(originalFilePath);
                        }
                    }
                    return await Update(_commodity);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }

        }
        public async Task<string> Upload(IFormFile fileInput, string filename)
        {
            try
            {
                string filepath = "";
                if (fileInput != null && fileInput.Length > 0)
                {
                    filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "pic", filename);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await fileInput.CopyToAsync(stream);
                    }
                }
                return filepath;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }

        }
    }
}
