using Microsoft.AspNetCore.Http;
using Data;
using Models;
using Microsoft.EntityFrameworkCore;
using Data.D;

namespace Operations.FileOperation
{
    public class FileOp
    {
        private readonly ApplicationDbContext _ctx;

        public FileOp(ApplicationDbContext context)
        {
            _ctx = context;
        }

        public async Task<bool> UploadFile(IEnumerable<IFormFile> files, int productid)
        {
            var images = new List<Image>();
            var product = await _ctx.Products.SingleOrDefaultAsync(p => p.Id == productid);
            try
            {
                foreach (var img in files)
                {
                    if (Path.GetExtension(img.FileName) != ".jpg" && Path.GetExtension(img.FileName) != ".png" && Path.GetExtension(img.FileName) != ".jpeg")
                    {
                        return false;
                    }
                    Image image = new Image() { Name = $"{Guid.NewGuid() + Path.GetExtension(img.FileName)}" };
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/images", image.Name);
                    var folderpath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/images");
                    if (!System.IO.Directory.Exists(folderpath))
                    {
                        System.IO.Directory.CreateDirectory(folderpath);
                    }
                    using (var fileStream = new FileStream(savePath, FileMode.Create))
                    {
                        img.CopyTo(fileStream);
                    }
                    image.ProductId = productid;

                    images.Add(image);
                }
                await _ctx.AddRangeAsync(images);
                if (string.IsNullOrEmpty(product.ImageName))
                {
                    product.ImageName = images.First().Name;
                    _ctx.Attach(product).State = EntityState.Modified;
                    await _ctx.SaveChangesAsync();
                }

                await _ctx.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }



        }
        public async Task<bool> DeleteFile(string filename)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/images", filename);

            try
            {
                if (File.Exists(filePath))
                {
                    var image = await _ctx.Images.Include(i => i.product).ThenInclude(p => p.Images).SingleOrDefaultAsync(i => i.Name == filename);
                    File.Delete(filePath);
                    _ctx.Images.Remove(image);
                    await _ctx.SaveChangesAsync();
                    if (filename == image.product.ImageName)
                    {
                        var product = image.product;
                        if (product.Images is not null)
                        {
                            product.ImageName = product.Images.First().Name;
                            _ctx.Attach(product).State = EntityState.Modified;
                            await _ctx.SaveChangesAsync();
                        }
                        else
                        {
                            product.ImageName = null;
                            _ctx.Attach(product).State = EntityState.Modified;
                            await _ctx.SaveChangesAsync();
                        }

                    }

                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                return false;
            }


        }
        public async Task<string> UploadSingleFile(IFormFile img)
        {
            try
            {
                if (Path.GetExtension(img.FileName) != ".jpg" && Path.GetExtension(img.FileName) != ".png" && Path.GetExtension(img.FileName) != ".jpeg")
                {
                    return "";
                }
                var name = Guid.NewGuid() + Path.GetExtension(img.FileName);
                string savePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/images", name);
                var folderpath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/images");
                if (!System.IO.Directory.Exists(folderpath))
                {
                    System.IO.Directory.CreateDirectory(folderpath);
                }
                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    img.CopyTo(fileStream);
                }

                return name;
            }
            catch (Exception)
            {

                return "";
            }



        }
        public async Task<bool> DeleteSingleFile(string filename)
        {
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/images", filename);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

         
             
              
        }
        ~FileOp()
        {
            _ctx.Dispose();
        }
    }

}
