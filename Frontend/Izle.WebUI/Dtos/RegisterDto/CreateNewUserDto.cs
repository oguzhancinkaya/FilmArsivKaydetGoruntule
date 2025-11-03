using System.ComponentModel.DataAnnotations;

namespace Izle.WebUI.Dtos.RegisterDto
{
	public class CreateNewUserDto
	{
		[Required(ErrorMessage = "Fotoğraf Bilgisi Gereklidir!")]
		public string UserImage { get; set; }

		[Required(ErrorMessage = "Ad Soyad Bilgisi Gereklidir!")]
        public string NameSurname { get; set; }

		[Required(ErrorMessage = "Kullanıcı Adı Bilgisi Gereklidir!")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Mail Bilgisi Gereklidir!")]
		public string Mail { get; set; }

		[Required(ErrorMessage = "Şifre Bilgisi Gereklidir!")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Şifre Tekrar Bilgisi Gereklidir!")]
		[Compare("Password",ErrorMessage ="Şifreler Uyuşmuyor")]
		public string ConfirmPassword { get; set; }

	}
}
