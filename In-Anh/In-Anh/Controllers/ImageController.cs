using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System;
using Newtonsoft.Json;
using MongoDB.Driver;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using In_Anh.Models;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using AngleSharp.Media;
using Image = SixLabors.ImageSharp.Image;
using Point = SixLabors.ImageSharp.Point;
using Color = SixLabors.ImageSharp.Color;
using System.Diagnostics;
using static MongoDB.Driver.WriteConcern;
using static In_Anh.Models.OrderModel;
using In_Anh.RabitMQ;
using In_Anh.Models.RabitMQModel;

namespace In_Anh.Controllers
{
    public class ImageController : BaseController
    {
        public ImageController(IConfiguration config, IImageMgDatabase setting, IRabitMQProducer rabitMQProducer) : base(config, setting, rabitMQProducer)
        {
        }


        // GET: ImageController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ImageController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }




        // POST: ImageController/Create
        [HttpPost]

        public async Task<ActionResult> CreateAsync(IFormCollection data)
        {
            try
            {
                var UserPhone = string.IsNullOrWhiteSpace(GetPhoneUserCookies().ToString()) ? "spam" : GetPhoneUserCookies().ToString();

                var localFile = _config["Cdn:LocalPath"];



                var year = DateTime.Now.Year;
                var month = DateTime.Now.Month;
                var day = DateTime.Now.Day;
                var type = 0;
                int.TryParse(data["type"].ToString(), out type);
                string nameType = Enum.GetName(typeof(ImageType), type) ?? "1x1";
                var id = GetOrderIdUserCookies();
                var path = UserPhone + "\\" + year + "-" + month + "-" + day + "\\" + id + "\\" + nameType + "\\";
                ;
                var filePath = localFile + path;

                if (data.Files.Count > 0)
                {
                    var file = data.Files[0];
                    if (file.Length > 0)
                    {
                        byte[] fileSream;

                       await using (var memoryStream = new MemoryStream())
                        {
                            file.OpenReadStream();
                            await file.CopyToAsync(memoryStream);
                            fileSream = memoryStream.ToArray();
                            memoryStream.Dispose();
                        }
                        _rabitMQProducer.SendProductMessage(new RabitMQSendData()
                        {
                            File = fileSream,
                            path = filePath,
                        });

                    }

                }



                //var listImg = new List<ImageModel>();

                //long size = files.Sum(f => f.Length);

                //try
                //{
                //    foreach (var formFile in files)
                //    {
                //        if (formFile.Length > 0)
                //        {

                //            if (!string.Equals(formFile.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
                //   !string.Equals(formFile.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
                //   !string.Equals(formFile.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
                //   !string.Equals(formFile.ContentType, "image/gif", StringComparison.OrdinalIgnoreCase) &&
                //   !string.Equals(formFile.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
                //   !string.Equals(formFile.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
                //            {
                //                return new JsonResult(new
                //                {
                //                    Code = 400,
                //                    Data = new { },
                //                    Message = "File Not Valid"
                //                });
                //            }
                //            var fileName = Guid.NewGuid().ToString() + ".jpg";
                //            var filePath = localFile + path + fileName;

                //            using (var memoryStream = new MemoryStream())
                //            {
                //                await formFile.CopyToAsync(memoryStream);
                //                var buffer = memoryStream.GetBuffer();
                //                string content = System.Text.Encoding.UTF8.GetString(buffer);
                //                if (Regex.IsMatch(content, @"<script|<cross\-domain\-policy",
                //                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                //                {
                //                    return new JsonResult(new
                //                    {
                //                        Code = 400,
                //                        Data = new { },
                //                        Message = "File Not Valid"
                //                    });
                //                }
                //                var Url = publicFile + path + fileName;


                //                if (!System.IO.Directory.Exists(localFile + path))
                //                {
                //                    Directory.CreateDirectory(localFile + path);
                //                }


                //                using (FileStream f = System.IO.File.Create(filePath))
                //                {

                //                    f.Dispose();
                //                }
                //                memoryStream.Position = 0;
                //                using (MagickImage image = new MagickImage(memoryStream))
                //                {
                //                    image.Format = MagickFormat.Jpeg;
                //                    image.Quality = 100;
                //                    image.Write(filePath);
                //                    image.Dispose();
                //                }
                //            }

                //        }



                //    }
                //}
                //catch (Exception e)
                //{

                //    return new JsonResult(new
                //    {
                //        Code = 400,
                //        Data = new { },
                //        Message = "Error System"
                //    });

                //}


                //PrintImages(listImg);
                return new JsonResult(new
                {
                    Code = 200,
                    Data = new { },
                    Message = "Success"
                });
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw;
            }

          

        }

        [HttpGet]
        public async Task<ActionResult> CreareOrderID(string orderID, string phone, string address, string name, string note)
        {
            try
            {
                var userGetOrder = _ordersCollection.FindAsync(x => x.Phone == phone).Result.FirstOrDefault();

                var detail = new OrderDetail()
                {
                    OrderId = orderID,
                    Address = address,
                    UserEmail = "",
                    Name = name,
                    Note = note,
                    IsActive = false,
                    Phone = phone,
                    DayOrder = new DateTime(DateTime.Now.Ticks),
                    Active = Active.Inactive
                    
                };
                if (userGetOrder == null)
                {
                    var orderDetail = new List<OrderDetail>
                    {
                        detail
                    };

                    await _ordersCollection.InsertOneAsync(new OrderModel()
                    {
                        ListDetail = orderDetail,
                        Phone = phone,
                    }); ;
                }
                else
                {
                    List<OrderDetail> orders = new List<OrderDetail>(userGetOrder.ListDetail)
                    {
                        detail
                    };
                    var filter = Builders<OrderModel>.Filter
     .Eq(x => x.Phone, phone);
                    var update = Builders<OrderModel>.Update
                        .Set(y => y.ListDetail, orders);
                    await _ordersCollection.UpdateOneAsync


                        (filter, update);
                }


                return new JsonResult(new
                {
                    Code = 200,
                    Data = new { },
                    Message = "success"
                });
            }
            catch (Exception e)
            {

                return new JsonResult(new
                {
                    Code = 200,
                    Data = new { message = e.Message },
                    Message = "success"
                });
            }
        }
        private async void PrintImages(List<ImageModel> imgs)
        {

            using (Image<Rgba32> image = new((int)MilimeterToPixel(210), (int)MilimeterToPixel(297)))
            {
                image.Mutate(x => x.BackgroundColor(Color.White));
                foreach (ImageModel im in imgs)
                {

                    if (im.Url.Count > 0)
                    {
                        foreach (var item in im.Url)
                        {
                            using (Image imsc = Image.Load(item))
                            {
                                if (im.Type == ImageType.t4x6)
                                {
                                    int w = (int)MilimeterToPixel(40);
                                    int h = (int)MilimeterToPixel(60);
                                    int howManyCountWidth = (int)MilimeterToPixel(210) / w;
                                    int howManyCountHeight = (int)MilimeterToPixel(297) / h;

                                    int fspacingx = (int)MilimeterToPixel(1);
                                    int spacingx = (int)MilimeterToPixel(2);

                                    int fspacingy = fspacingx;
                                    int spacingy = ((int)MilimeterToPixel(297) - (howManyCountHeight * h) - fspacingy) / howManyCountHeight;
                                    imsc.Mutate(c => c.Resize(w, h));
                                    var poin = new Point();
                                    var index = im.Url.IndexOf(item);
                                    var posionx = 0;
                                    var posiony = 0;
                                    var numberMod = index % howManyCountWidth;
                                    var numberNoMod = index / howManyCountWidth;

                                    var totalSpcx = 0;
                                    var totalSpcy = 0;

                                    if (numberMod == 0)
                                    {
                                        totalSpcx = fspacingx;
                                    }
                                    else
                                    {
                                        totalSpcx = fspacingx + numberMod * (w + spacingx);
                                    }
                                    if (numberNoMod == 0)
                                    {
                                        totalSpcy = fspacingy;
                                    }
                                    else
                                    {
                                        totalSpcy = fspacingy + numberNoMod * (h + spacingy);
                                    }


                                    poin.X = totalSpcx;
                                    poin.Y = totalSpcy;
                                    image.Mutate(c => c.DrawImage(imsc, poin, 1));
                                    imsc.Dispose();
                                }
                            }
                        }
                    }
                }
                // Resize the image in place and return it for chaining.
                // 'x' signifies the current image processing context.


                // The library automatically picks an encoder based on the file extension then
                // encodes and write the data to disk.
                // You can optionally set the encoder to choose.

                image.Save("D:\\imgs\\img.png");
                //Process p = new Process();

                //p.StartInfo.Verb = "Print";
                //p.Start();
                image.Dispose();
            }
        }

        private float MilimeterToPixel(int valueMl, float dpi = 221)
        {
            return 0.03937007F * dpi * valueMl * 10;
        }

    }
}
