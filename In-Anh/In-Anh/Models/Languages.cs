namespace In_Anh.Models
{
    public class Languages
    {
        public string Home { get; set; }
        public string Login { get; set; }
        public string ImagePrint { get; set; }
        public string FilePrint { get; set; }
        public string ChooseSize { get; set; }


        public Languages LanguageVN()                                           
        {
            return new Languages()
            {
                Home = "Trang Chủ",
                Login = "Đăng Nhập",
                ImagePrint = "In Ảnh",
                FilePrint = "In File",
                ChooseSize ="Thêm Size"
            };
        }

        public Languages LanguageEN()
        {
            return new Languages()
            {
                Home = "Home",
                Login = "Login",
                ImagePrint = "Image Print",
                FilePrint = "File Print",
                ChooseSize = "More Size"
            };
        }

    }

   
    
}
