//using Microsoft.AspNetCore.Mvc;
//using System.Drawing;
//using System.IO;
//using System.Threading.Tasks;
//using System.Web;


//namespace Cropper
//{
//    public class ImageResult : IActionResult
//    {
//        private readonly Image _image;
//        private readonly string _name;

//        public ImageResult(Image image, string name)
//        {
//            _image = image;
//            _name = name;
//        }

//        public TestActionResult(TestResult result)
//        {
//            _result = result;
//        }

//        public override void ExecuteResult(ControllerContext context)
//        {
//            var ms = new MemoryStream();
//            _image.Save(ms, _image.RawFormat);
//            ms.Position = 0;

//            var fileStreamResult = new FileStreamResult(ms, MimeMapping.GetMimeMapping(_name));
//            fileStreamResult.ExecuteResult(context);
//        }

//        public Task ExecuteResultAsync(ActionContext context)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}