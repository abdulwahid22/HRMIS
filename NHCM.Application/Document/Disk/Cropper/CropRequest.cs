namespace NHCM.Application.Document.Disk.Cropper.Models
{
    public class CropRequest : UploadedFile
    {
        /// <summary>
        /// your image path (the one we recieved after successfull upload)
        /// </summary>
        public string imgUrl { get; set; }
        // <summary>
        // your image original width(the one we recieved after upload)
        /// </summary>
        public int imgInitW { get; set; }
        /// <summary>
        /// your image original height (the one we recieved after upload)
        /// </summary>
        public int imgInitH { get; set; }
        /// <summary>
        /// your new scaled image width
        /// </summary>
        public double imgW { get; set; }
        /// <summary>
        /// your new scaled image height
        /// </summary>
        public double imgH { get; set; }
        /// <summary>
        /// top left corner of the cropped image in relation to scaled image
        /// </summary>
        public int imgX1 { get; set; }
        /// <summary>
        /// top left corner of the cropped image in relation to scaled image
        /// </summary>
        public int imgY1 { get; set; }
        /// <summary>
        /// cropped image width
        /// </summary>
        public double cropW { get; set; }
        /// <summary>
        /// cropped image height
        /// </summary>
        public double cropH { get; set; }
    }
}
