namespace In_Anh.Models
{
    public class Languages
    {
        public string Home { get; set; }
        public string Login { get; set; }
        public string ImagePrint { get; set; }
        public string FilePrint { get; set; }
        public string ChooseSize { get; set; }
        public string Print { get; set; }
        public string Image { get; set; }
        public string All { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string Ward { get; set; }
        public string Note { get; set; }


        public Languages LanguageVN()                                           
        {
            return new Languages()
            {
                Home = "Trang Chủ",
                Login = "Đăng Nhập",
                ImagePrint = "In Ảnh",
                FilePrint = "In File",
                ChooseSize ="Thêm Size",
                Print ="In",
                Image ="Ảnh",
                All ="Tất Cả",
                Address ="Địa Chỉ",
                Name ="Tên",
                Phone = "Số Điện Thoại",
                Province = "Tỉnh Thành",
                Ward ="Quận, Huyện",
                Note = "Ghi Chú"
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
                ChooseSize = "More Size",
                All= "All",
                Image ="Image",
                Print ="Print",
                Address = "Address",
                Name = "Name",
                Phone = "Phone",
                Province = "Province",
                Ward = "Ward",
                Note= "Note"
            };
        }

    }

   
    
}
